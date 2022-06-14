using System;
using FluentAssertions;
using Xunit;

namespace TestDoubles
{
    public class Stub
    {
        [Fact]
        public void Should_Divide_A_Numerator_By_A_Denominator_When_Authorization_Is_Accepted()
        {
            var authorizerStub = new AllowAccessAuthorizer();
            var calculator = new Calculator(authorizerStub);

            calculator.Divide(9, 3)
                .Should()
                .Be(3);
        }

        [Fact]
        public void Should_Divide_A_Numerator_By_A_Denominator_When_Authorization_Is_Denied()
        {
            var authorizerStub = new DenyAccessAuthorizer();
            var calculator = new Calculator(authorizerStub);

            calculator.Invoking(_ => _.Divide(9, 3))
                .Should()
                .Throw<UnauthorizedAccessException>();
        }

        private class Calculator
        {
            private readonly IAuthorizer _authorizer;

            public Calculator(IAuthorizer authorizer)
            {
                _authorizer = authorizer;
            }

            public int Divide(int numerator, int denominator)
                => CheckAuthorization(DivideFunction(numerator, denominator));

            private int CheckAuthorization(Func<int> onSuccess) =>
                _authorizer.Authorize()
                    ? onSuccess()
                    : throw new UnauthorizedAccessException();

            private static Func<int> DivideFunction(int numerator, int denominator) =>
                () => denominator == 0
                    ? throw new ArgumentException("Denominator can not be zero")
                    : numerator / denominator;
        }

        private interface IAuthorizer
        {
            bool Authorize();
        }

        private class AllowAccessAuthorizer : IAuthorizer
        {
            public bool Authorize() => true;
        }

        private class DenyAccessAuthorizer : IAuthorizer
        {
            public bool Authorize() => false;
        }
    }
}