using System;
using FluentAssertions;
using Xunit;
using static System.Linq.Enumerable;
using static FizzBuzz.Tests.RandomExtensions;

namespace FizzBuzz.Tests
{
    public class FizzBuzzShould
    {
        private const string ResultFile = "results.txt";
        private static FizzBuzz _fizzBuzz;

        [Theory]
        [InlineData(3)]
        [InlineData(12)]
        [InlineData(81)]
        public void Return_Fizz_For_Multiples_Of3(int value)
        {
            _fizzBuzz = FizzBuzz.New();
            _fizzBuzz.Convert(value)
                .Should()
                .Be("Fizz");
        }

        [Theory]
        [InlineData(5)]
        [InlineData(25)]
        [InlineData(95)]
        public void Return_Buzz_For_Multiples_Of5(int value) =>
            _fizzBuzz.Convert(value)
                .Should()
                .Be("Buzz");

        [Theory]
        [InlineData(15)]
        [InlineData(45)]
        [InlineData(75)]
        public void Return_Buzz_For_Multiples_Of15(int value) =>
            _fizzBuzz.Convert(value)
                .Should()
                .Be("FizzBuzz");

        [Theory]
        [InlineData(1, "Simple Value : 1")]
        [InlineData(23, "Simple Value : 23")]
        [InlineData(76, "Simple Value : 76")]
        public void Return_The_Value_For_Others(int value, string expectedResult) =>
            _fizzBuzz.Convert(value)
                .Should()
                .NotBeNull("Should return something")
                .And
                .StartWith("Simple Value :");

        [Fact]
        public void Return_Valid_String()
        {
            var random = new Random();
            var randomInt = ValidInt(random);

            _fizzBuzz.Convert(randomInt)
                .Should()
                .BeOneOf("Fizz", "Buzz", "FizzBuz");
        }

        [Fact]
        public void Write_Results_With_Big_FizzBuzz_For_Human_Validation()
        {
            FileUtils.DeleteFile(ResultFile);

            const int lowerBound = 1;
            const int upperBound = 50_000;

            var bigFizzBuzz = FizzBuzz.New(lowerBound, upperBound);

            var result =
                Range(lowerBound, upperBound)
                    .AsEnumerable()
                    .Select(v => bigFizzBuzz.Convert(v))
                    .Aggregate("", (acc, value) => $"{acc}{value}{Environment.NewLine}");

            FileUtils.AppendToFile(ResultFile, result);
            FileUtils.CountLines(ResultFile)
                .Should()
                .Be(upperBound);
        }
    }
}