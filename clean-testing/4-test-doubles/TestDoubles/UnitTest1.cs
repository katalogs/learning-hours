using System;
using FluentAssertions;
using Xunit;

namespace TestDoubles;

public class UnitTest1
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
}

public class Calculator

{
    public int Divide(int numerator, int denominator)
    {
        throw new System.NotImplementedException();
    }
}