# Discriminated Unions
## Learning Goals
`Understand how Discriminated Unions can help write more explicit code`

## Connect - Improve or NOT improve ? (10 min)
Identify what can be improved in the code below 

> Think as a consumer of this class

```c#
namespace Optivem.Kata.Banking.Core.UseCases.OpenAccount
{
    public class OpenAccountUseCase : IUseCase<OpenAccountRequest, OpenAccountResponse>
    {
        private readonly IAccountNumberGenerator _accountNumberGenerator;
        private readonly IBankAccountRepository _bankAccountRepository;

        public OpenAccountUseCase(IAccountNumberGenerator accountNumberGenerator,
            IBankAccountRepository bankAccountRepository)
        {
            _accountNumberGenerator = accountNumberGenerator;
            _bankAccountRepository = bankAccountRepository;
        }

        public Task<OpenAccountResponse> HandleAsync(OpenAccountRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                throw new ValidationException(ValidationMessages.FirstNameEmpty);
            }

            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                throw new ValidationException(ValidationMessages.LastNameEmpty);
            }

            var balance = Balance.From(request.Balance);

            var accountNumber = _accountNumberGenerator.Next();


            var bankAccount = new BankAccount(accountNumber, request.FirstName, request.LastName, balance);
            _bankAccountRepository.Add(bankAccount);

            var response = new OpenAccountResponse
            {
                AccountNumber = accountNumber.Value,
            };

            return Task.FromResult(response);
        }
    }
}
```

### Debriefing - Code Smells
- This code contains `lies`
  - If you take a look at the API level this class looks like this

```c#
public class OpenAccountUseCase : IUseCase<OpenAccountRequest, OpenAccountResponse> 
{
    public Task<OpenAccountResponse> HandleAsync(OpenAccountRequest request) { ... }
}
```
- So in terms of function signature `OpenAccountRequest request -> Task<OpenAccountResponse>`
- But in reality it can break your application flow by throwing exceptions 
  - without saying it
  - without forcing callers to treat explicitly those lies

```c#
public Task<OpenAccountResponse> HandleAsync(OpenAccountRequest request)
{
    // Lie
    if (string.IsNullOrWhiteSpace(request.FirstName))
    {
        throw new ValidationException(ValidationMessages.FirstNameEmpty);
    }
    // Lie
    if (string.IsNullOrWhiteSpace(request.LastName))
    {
        throw new ValidationException(ValidationMessages.LastNameEmpty);
    }

    // Can throw an ArgumentException -> "Balance must be positive"
    var balance = Balance.From(request.Balance);
    var accountNumber = _accountNumberGenerator.Next();

    var bankAccount = new BankAccount(accountNumber, request.FirstName, request.LastName, balance);
    _bankAccountRepository.Add(bankAccount);

    var response = new OpenAccountResponse
    {
        AccountNumber = accountNumber.Value,
    };

    return Task.FromResult(response);
}
```

## Concepts

### Presentation : Discriminated Unions in a few words
- Present what is `Discriminated Unions` : https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/discriminated-unions
- Present the `OneOf` library in C# : https://github.com/mcintyre321/OneOf 

### Demo (10 min)
Refactor the `UseCase` class to be more explicit in terms of return type and control the flow from the caller :
- Start by the `Balance` class
  - Change `From` signature to return a `OneOf<InvalidBalance, Balance>`

```c#
using OneOf;

namespace discriminatedUnions.demo
{
    public record Balance
    {
        private readonly int value;

        private Balance(int balance)
            => value = balance;

        public static OneOf<InvalidBalance, Balance> From(int balance)
            => (balance <= 0) ?
                new InvalidBalance("Balance must be positive") :
                new Balance(balance);
    }

    public record InvalidBalance(string Message);
}
```
- Adapt the caller : `OpenAccountUseCase`
  - Use `Match` to treat the result

```c#
public class OpenAccountUseCase : IUseCase<OpenAccountRequest, OpenAccountResponse>
{
    public OpenAccountResponse Handle(OpenAccountRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName))
        {
            throw new ValidationException("First name should not be Empty");
        }

        if (string.IsNullOrWhiteSpace(request.LastName))
        {
            throw new ValidationException("Last name should not be Empty");
        }

        return Balance.From(request.Balance)
            .Match(invalid => throw new ArgumentException("Balance must be positive"),
                balance => CreateResponse(request, balance));
    }

    private static OpenAccountResponse CreateResponse(OpenAccountRequest request, Balance balance)
        => new(new BankAccount("New Account Number", request.FirstName!, request.LastName!, balance).AccountNumber);
}
```
- We have no impact on the tests for now
- So let's adapt the Handle method to return a descriptive result :

```c#
using OneOf;

namespace discriminatedUnions.demo
{

    public class OpenAccountUseCase : IUseCase<OpenAccountRequest, OpenAccountResponse>
    {
        public OneOf<InvalidBalance, InvalidRequest, OpenAccountResponse> Handle(OpenAccountRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
                return new InvalidRequest("First name should not be Empty");
            if (string.IsNullOrWhiteSpace(request.LastName)) return new InvalidRequest("Last name should not be Empty");

            return Balance.From(request.Balance)
                .Match(invalid => invalid,
                    // Cast is needed for compiler only...
                    balance => (OneOf<InvalidBalance, InvalidRequest, OpenAccountResponse>) CreateResponse(request,
                        balance));
        }

        private static OpenAccountResponse CreateResponse(OpenAccountRequest request, Balance balance)
            => new(new BankAccount("New Account Number", request.FirstName!, request.LastName!, balance).AccountNumber);
    }

    public record InvalidRequest(string Message);
} 
```

- What is the impact on the caller? a.k.a the test class
    - We need to plug the assertion on `OneOf`

```c#
using System;
using discriminatedUnions.demo;
using FluentAssertions;
using Xunit;
using static discriminatedUnions_demo.Tests.OpenAccountRequestBuilder;

namespace discriminatedUnions_demo.Tests
{

    public class OpenAccountUseCase_Should
    {
        private readonly OpenAccountUseCase _useCase = new();

        public class Return_An_Invalid_Request : OpenAccountUseCase_Should
        {
            [Fact]
            public void When_First_Name_Is_Null()
                => AssertValidationExceptionIsThrown(requestBuilder => requestBuilder.WithoutFirstName(), "First name");

            [Fact]
            public void When_First_Name_Is_Empty()
                => AssertValidationExceptionIsThrown(requestBuilder => requestBuilder.WithAnEmptyFirstName(),
                    "First name");

            [Fact]
            public void When_Last_Name_Is_Null()
                => AssertValidationExceptionIsThrown(requestBuilder => requestBuilder.WithoutLastName(), "Last name");

            [Fact]
            public void When_Last_Name_Is_Empty()
                => AssertValidationExceptionIsThrown(requestBuilder => requestBuilder.WithAnEmptyLastName(),
                    "Last name");

            private void AssertValidationExceptionIsThrown(
                Func<OpenAccountRequestBuilder, OpenAccountRequestBuilder> setup, string propertyName)
            {
                var request = setup(ANewRequest())
                    .Build();

                _useCase.Handle(request)
                    .AsT1.Message.Should()
                    .Be($"{propertyName} should not be Empty");
            }
        }

        public class Return_An_InvalidBalance : OpenAccountUseCase_Should
        {
            [Fact]
            public void When_Balance_Is_Zero()
                => AssertValidationExceptionIsThrown(0);

            [Fact]
            public void When_Balance_Is_Negative()
                => AssertValidationExceptionIsThrown(-90);

            private void AssertValidationExceptionIsThrown(int balance)
            {
                var request = ANewRequest()
                    .WithABalance(balance)
                    .Build();

                _useCase.Handle(request)
                    .AsT0.Message.Should()
                    .Be("Balance must be positive");
            }
        }

        [Fact]
        public void Return_An_Account_Number()
            => _useCase.Handle(ANewRequest()
                    .Build())
                .AsT2.AccountNumber.Should()
                .Be("New Account Number");
    }
}
```

## Concrete Practice (35 min)

Ask attendees to: 
- Explore the code
- Identify where the code contains lies 
- Refactor using `OneOf`

### A word on Discriminated Unions in F#
```f#
type conversionResult =
    | Money of Money
    | MissingExchangeRate of string

type evaluationResult =
    | Money of Money
    | ExchangeRates of string List
```

### Solution
A "solution" using `OneOf` library is available in the branch `OneOf` available [here](https://github.com/ythirion/oneof-money-problem/tree/OneOf/money-problem.Domain)

## Conclusion (5min) - Impact
According to you, what impact would the use of Discriminated Unions have on your code?