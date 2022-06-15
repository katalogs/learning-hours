using System;
using FluentAssertions;
using Xunit;

namespace TestDoubles.Stub
{
    public class StubTests
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
    }
}