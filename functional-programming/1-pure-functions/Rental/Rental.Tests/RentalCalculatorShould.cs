using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using static System.Environment;
using static Rental.Tests.RentalBuilder;

namespace Rental.Tests
{
    public class RentalCalculatorShould
    {
        private static IEnumerable<Rental> NoRentals() => new List<Rental>();

        private static IEnumerable<Rental> SomeRentals() => new[]
        {
            ARental()
                .OnThe(new DateOnly(2020, 10, 9))
                .Labelled("Le Refuge des Loups (LA BRESSE)")
                .For(1089.9)
                .Build(),
            ARental()
                .OnThe(new DateOnly(2020, 10, 12))
                .Labelled("Au pied de la Tour (NOUILLORC)")
                .For(1276.45)
                .Build(),
            ARental()
                .OnThe(new DateOnly(2020, 10, 24))
                .Labelled("Le moulin du bonheur (GLANDAGE)")
                .For(670.89)
                .Build()
        };

        [Fact]
        public void Throw_An_Exception_When_No_Rentals()
        {
            var calculator = new RentalCalculator(NoRentals());
            calculator.Invoking(_ => _.Calculate())
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("No rentals on which perform calculation");
        }

        [Fact]
        public void CalculateRentalsAndFormatStatement()
        {
            var calculator = new RentalCalculator(SomeRentals());
            var statement = calculator.Calculate();

            calculator.IsCalculated
                .Should()
                .BeTrue();

            calculator.Amount
                .Should()
                .BeApproximately(3037.24, 0.001);

            statement.Should()
                .Be(
                    "09-10-2020 : Le Refuge des Loups (LA BRESSE) | 1089.9" + NewLine +
                    "12-10-2020 : Au pied de la Tour (NOUILLORC) | 1276.45" + NewLine +
                    "24-10-2020 : Le moulin du bonheur (GLANDAGE) | 670.89" + NewLine +
                    "Total amount | 3037.2400000000002"
                );
        }

        [Fact]
        public void CalculateRentals()
        {
            var calculator = new RentalCalculator(SomeRentals());
            double totalAmount = calculator.Calculate();

            totalAmount
                .Should()
                .BeApproximately(3037.24, 0.001);

        }

        [Fact]
        public void FormatStatement()
        {
            var calculator = new RentalCalculator(SomeRentals());
            var statement = calculator.Calculate();

            calculator.IsCalculated
                .Should()
                .BeTrue();

            calculator.Amount
                .Should()
                .BeApproximately(3037.24, 0.001);

            statement.Should()
                .Be(
                    "09-10-2020 : Le Refuge des Loups (LA BRESSE) | 1089.9" + NewLine +
                    "12-10-2020 : Au pied de la Tour (NOUILLORC) | 1276.45" + NewLine +
                    "24-10-2020 : Le moulin du bonheur (GLANDAGE) | 670.89" + NewLine +
                    "Total amount | 3037.2400000000002"
                );
        }


    }
}