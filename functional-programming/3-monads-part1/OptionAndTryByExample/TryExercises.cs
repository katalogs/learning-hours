using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using LanguageExt;
using Xunit;
using static LanguageExt.Prelude;

namespace OptionAndTryByExample
{
    public class TryExercises
    {
        private const string SuccessMessage = "I m a fucking genius the result is ";

        private static Try<int> Divide(int x, int y)
            => Try(() => x / y);

        [Fact]
        public void GetTheResultOfDivide()
        {
            // Divide x = 9 by y = 2
            var tryResult = Divide(9, 2);
            var result = 0;

            result.Should().Be(4);
            tryResult
                .Should()
                .BeSuccess(_ => _.Should().Be(4))
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

            result
                .Should()
                .Be(7);
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

            result
                .Should()
                .Be(0);
        }

        [Fact]
        public void MapTheFailure()
        {
            // Divide x by 0, log the failure message to the console and get 0
            var x = 1;

            var result = -1;

            result
                .Should()
                .Be(0);
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

            result
                .Should()
                .Be(1);
        }

        [Fact]
        public void TryAndReturnOption()
        {
            // Create a Divide function that return an Option on Divide
            // If something fails -> return None
            // Can be useful sometimes
            var result = 0;
            result
                .Should()
                .Be(3);
        }

        [Fact]
        public async Task TryOnAsync()
        {
            // Create a Divide function that return a TryAsync
            var result = 1;
            result
                .Should()
                .Be(0);
        }
    }
}