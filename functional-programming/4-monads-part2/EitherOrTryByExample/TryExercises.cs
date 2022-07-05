using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using LanguageExt;
using Xunit;

namespace EitherOrTryByExample
{
    public class TryExercises
    {
        private static Try<int> Divide(int x, int y)
            => () => x / y;

        [Fact]
        public void GetTheResultOfDivide()
        {
            // Divide x = 9 by y = 2
            Try<int> tryResult = () => 0;

            tryResult
                .Should()
                .BeSuccess(result => result.Should().Be(4))
                .And
                .IsDefault()
                .Should()
                .BeFalse();
        }

        [Fact]
        public void MapTheResultOfDivide()
        {
            // Divide x = 9 by y = 2 and add z to the result
            var z = 3;
            var result = 0;

            result.Should().Be(7);
        }

        [Fact]
        public void DivideByZeroIsAlwaysAGoodIdea()
        {
            // Divide x by 0 and get the result
            var x = 1;

            var divideByZero = () => 0;

            divideByZero
                .Should()
                .Throw<DivideByZeroException>();
        }

        [Fact]
        public void DivideByZeroOrElse()
        {
            // Divide x by 0, on exception returns 0
            var x = 1;
            var result = -1;

            result.Should().Be(0);
        }

        [Fact]
        public void MapTheFailure()
        {
            // Divide x by 0, log the failure message to the console
            // And get 0
            var x = 1;
            var result = -1;

            result.Should().Be(0);
        }

        [Fact]
        public void MapTheSuccess()
        {
            // Divide x by y
            // log the failure message to the console
            // Log your success to the console
            // Get the result or 0 if exception
            var x = 8;
            var y = 4;

            var result = -1;

            result.Should().Be(2);
        }

        [Fact]
        public void MapTheSuccessWithoutMatch()
        {
            // Divide x by y
            // log the failure message to the console
            // Log your success to the console
            // Get the result or 0 if exception
            // You are not authorized to use the Match method
            var x = 8;
            var y = 4;

            var result = -1;

            Assert.Equal(2, result);
        }

        [Fact]
        public void ChainTheTry()
        {
            // Divide x by y
            // Chain 2 other calls to divide with x = previous Divide result
            // log the failure message to the console
            // Log your success to the console
            // Get the result or 0 if exception
            var x = 27;
            var y = 3;

            var result = -1;

            result.Should().Be(1);
        }

        [Fact]
        public void TryAndReturnOption()
        {
            // Create a Divide function that return an Option on Divide
            // If something fails -> return None
            // Can be useful sometimes
            var result = 0;
            result.Should().Be(3);
        }

        [Fact]
        public async Task TryOnAsync()
        {
            // Create a Divide function that return a TryAsync
            var result = 1;
            result.Should().Be(0);
        }
    }
}