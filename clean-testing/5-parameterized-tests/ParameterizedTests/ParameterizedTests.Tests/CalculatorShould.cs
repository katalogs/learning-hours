using System;
using Xunit;

namespace ParameterizedTests.Tests
{
    public class CalculatorShould
    {
        [Fact]
        public void SupportAdd()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Calculate(9, 3, Calculator.Add);

            // Assert
            Assert.Equal(12, result);
        }

        [Fact]
        public void SupportMultiply()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Calculate(3,76, Calculator.Multiply);

            // Assert
            Assert.Equal(228, result);
        }
    
        [Fact]
        public void SupportDivide()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Calculate(9,3, Calculator.Divide);

            // Assert
            Assert.Equal(3, result);
        }
    
        [Fact]
        public void SupportSubtract()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Calculate(9,3, Calculator.Subtract);

            // Assert
            Assert.Equal(6, result);
        }
    
        [Fact]
        public void FailWhenOperatorNotSupported()
        {
            var calculator = new Calculator();
            var exception = Assert.Throws<ArgumentException>(() => calculator.Calculate(9, 3, "UnsupportedOperator"));
        
            Assert.Equal("Not supported operator", exception.Message);
        }

        [Fact]
        public void FailWhenDividingByZero()
        {
            var calculator = new Calculator();
            var exception = Assert.Throws<DivideByZeroException>(() => calculator.Calculate(2, 0, Calculator.Divide));
        
            Assert.Equal("Attempted to divide by zero.", exception.Message);
        }
    }
}