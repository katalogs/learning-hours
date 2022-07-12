using System;
using FluentAssertions;
using Xunit;

namespace PlayWithFunctors
{
    public class FunctionsAreFunctors
    {
        private static int Add(int x, int y) => x + y;
        private static int Multiply(int x, int y) => x * y;

        private int Add1AndDouble(int x)
        {
            return Add1(x).Map(Double);
            //return Double(Add1(x));
        }

        private static int Add1(int x) => Add(x, 1);

        private static int Double(int x) => Multiply(x, 2);

        [Fact]
        public void Add1AndDoubleIt()
        {
            // Create an Add1 function based on Add function
            // Create a Double function based on Multiply
            // Compose the 2 functions together to implement the Add1AndDouble function
            Add1AndDouble(2)
                .Should()
                .Be(6);
        }

        [Fact]
        public void AddXAndDoubleItInDouble()
        {
            // Use the Double and the Add function to implement it
            AddXtoYAndDouble(2, 7)
                .Should()
                .Be(18);
        }

        private int AddXtoYAndDouble(int x, int y)
        {
            throw new NotImplementedException();
        }
    }

    public static class IntExtensions
    {
        public static int Map(this int result, Func<int, int> mapFunc)
        {
            return mapFunc(result);
        }
    }
}