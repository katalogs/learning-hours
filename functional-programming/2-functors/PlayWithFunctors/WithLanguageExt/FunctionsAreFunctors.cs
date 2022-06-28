using System;
using FluentAssertions;
using Xunit;

namespace PlayWithFunctors.WithLanguageExt
{
    public class FunctionsAreFunctors
    {
        private static readonly Func<int, int, int> Add = (x, y) => x + y;
        private static readonly Func<int, int, int> Multiply = (x, y) => x * y;

        [Fact]
        public void Add1AndDoubleIt()
        {
            // Create an Add1 function based on Add function
            // Create a Double function based on Multiply
            // Compose the 2 functions together to implement the Add1AndDouble function

            int Add1AndDouble(int x) => 0;
            Add1AndDouble(2).Should().Be(6);
        }
    }
}