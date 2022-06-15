using System;

namespace TestDoubles.Dummy
{
    public class Calculator
    {
        public int Divide(int numerator, int denominator)
            => denominator == 0
                ? throw new ArgumentException("Denominator can not be zero")
                : numerator / denominator;
    }
}