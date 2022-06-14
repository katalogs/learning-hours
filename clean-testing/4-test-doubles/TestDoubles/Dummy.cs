using System;
using FluentAssertions;
using Xunit;

namespace TestDoubles
{
    public class Dummy
    {
        private readonly Calculator _calculator = new();

        [Fact]
        public void Should_Throw_An_ArgumentException_When_Divide_By_Zero()
        {
            const int numerator = 9;
            const int zero = 0;

            _calculator.Invoking(_ => _.Divide(numerator, zero))
                .Should()
                .Throw<ArgumentException>();
        }

        private class Calculator

        {
            public int Divide(int numerator, int denominator)
                => denominator == 0
                    ? throw new ArgumentException("Denominator can not be zero")
                    : numerator / denominator;
        }
    }
}