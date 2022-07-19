using System;
using System.ComponentModel.DataAnnotations;
using discriminatedUnions.demo;
using FluentAssertions;
using Xunit;
using static discriminatedUnions_demo.Tests.OpenAccountRequestBuilder;

namespace discriminatedUnions_demo.Tests
{
    public class OpenAccountUseCase_Should
    {
        private readonly OpenAccountUseCase _useCase = new();

        public class Throw_A_Validation_Exception : OpenAccountUseCase_Should
        {
            [Fact]
            public void When_First_Name_Is_Null()
                => AssertValidationExceptionIsThrown(
                    requestBuilder => requestBuilder.WithoutFirstName(),
                    "First name"
                );

            [Fact]
            public void When_First_Name_Is_Empty()
                => AssertValidationExceptionIsThrown(
                    requestBuilder => requestBuilder.WithAnEmptyFirstName(),
                    "First name"
                );

            [Fact]
            public void When_Last_Name_Is_Null()
                => AssertValidationExceptionIsThrown(
                    requestBuilder => requestBuilder.WithoutLastName(),
                    "Last name"
                );

            [Fact]
            public void When_Last_Name_Is_Empty()
                => AssertValidationExceptionIsThrown(
                    requestBuilder => requestBuilder.WithAnEmptyLastName(),
                    "Last name"
                );

            private void AssertValidationExceptionIsThrown(
                Func<OpenAccountRequestBuilder, OpenAccountRequestBuilder> setup,
                string propertyName)
            {
                var request = setup(ANewRequest()).Build();

                _useCase.Invoking(_ => _.Handle(request))
                    .Should()
                    .Throw<ValidationException>()
                    .WithMessage($"{propertyName} should not be Empty");
            }
        }

        public class Throw_An_Argument_Exception : OpenAccountUseCase_Should
        {
            [Fact]
            public void When_Balance_Is_Zero() => AssertValidationExceptionIsThrown(0);

            [Fact]
            public void When_Balance_Is_Negative() => AssertValidationExceptionIsThrown(-90);
            
            private void AssertValidationExceptionIsThrown(int balance)
            {
                var request = ANewRequest()
                    .WithABalance(balance)
                    .Build();

                _useCase.Invoking(_ => _.Handle(request))
                    .Should()
                    .Throw<ArgumentException>()
                    .WithMessage("Balance must be positive");
            }
        }

        [Fact]
        public void Return_An_Account_Number()
            => _useCase.Handle(ANewRequest()
                    .Build())
                .AccountNumber
                .Should()
                .Be("New Account Number");
    }

    public class OpenAccountRequestBuilder
    {
        private const string Empty = "    ";
        private string? _firstName = "John";
        private string? _lastName = "Doe";
        private int _balance = 300;

        public static OpenAccountRequestBuilder ANewRequest() => new();

        public OpenAccountRequestBuilder WithoutFirstName()
        {
            _firstName = null;
            return this;
        }

        public OpenAccountRequestBuilder WithAnEmptyFirstName()
        {
            _firstName = Empty;
            return this;
        }

        public OpenAccountRequestBuilder WithoutLastName()
        {
            _lastName = null;
            return this;
        }

        public OpenAccountRequestBuilder WithAnEmptyLastName()
        {
            _lastName = Empty;
            return this;
        }
        
        public OpenAccountRequestBuilder WithABalance(int balance)
        {
            _balance = balance;
            return this;
        }

        public OpenAccountRequest Build() => new(_firstName, _lastName, _balance);
    }
}
