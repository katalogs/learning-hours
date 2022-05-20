# Discriminated Unions
## Learning Goals
`Understand how Discriminated Unions can help write more explicit code`

## Preparation
- Use the ["OneOf money problem"](https://github.com/ythirion/oneof-money-problem/) in C#

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

## Concepts

### Presentation : Discriminated Unions in a few words
- Present what is `Discriminated Unions` : https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/discriminated-unions
- Present the `OneOf` library in C# : https://github.com/mcintyre321/OneOf 

### Demo (10 min)
Refactor the `UseCase` class to be more explicit in terms of return type and control the flow from the caller :
- Start by the `Balance` class
- Change `From` signature to return a `OneOf<InvalidBalance, Balance>`
- Adapt the caller

## Concrete Practice (35 min)

Ask attendees to: 
- Explore the code
- Identify where the code contains lies 
- Refactor using `OneOf`


Discriminated Unions in F# :
```f#
type conversionResult =
    | Money of Money
    | MissingExchangeRate of string

type evaluationResult =
    | Money of Money
    | ExchangeRates of string List
```

## Conclusion (5min) - Impact
According to you, what impact would the use of Discriminated Unions have on your code?