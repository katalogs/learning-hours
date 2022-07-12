using System;
using FluentAssertions;
using LanguageExt;
using Xunit;

namespace PlayWithFunctors.WithLanguageExt
{
    public class FunctionsAreFunctors
    {
        private static readonly Func<int, int, int> Add = (x, y) => x + y;
        private static readonly Func<int, int, int> Multiply = (x, y) => x * y;

        private static Func<int, int> Add1 = x => Add(x, 1);
        private static Func<int, int> Double = x => Multiply(x, 2);

        [Fact]
        public void Add1AndDoubleIt()
        {
            // Create an Add1 function based on Add function
            // Create a Double function based on Multiply
            // Compose the 2 functions together to implement the Add1AndDouble function
            
            
            int Add1AndDouble(int x) => Add1.Compose(Double)(x);
            Add1AndDouble(2).Should().Be(6);
        }
    }
}