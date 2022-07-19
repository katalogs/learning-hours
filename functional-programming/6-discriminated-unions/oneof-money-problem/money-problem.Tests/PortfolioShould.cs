using System;
using FluentAssertions;
using money_problem.Domain;
using Xunit;
using static money_problem.Domain.Currency;

namespace money_problem.Tests
{
    public class PortfolioShould
    {
        private readonly Bank _bank =
            Bank.WithExchangeRate(EUR, USD, 1.2)
                .AddExchangeRate(USD, KRW, 1100);

        [Fact(DisplayName = "5 USD + 10 USD = 15 USD")]
        public void Add()
        {
            var portfolio = 5d.Dollars().AddToPortfolio(10d.Dollars());
            portfolio.Evaluate(_bank, USD)
                .Should()
                .Be(15d.Dollars());
        }

        [Fact(DisplayName = "5 USD + 10 EUR = 17 USD")]
        public void AddDollarsAndEuros()
        {
            var portfolio = 5d.Dollars().AddToPortfolio(10d.Euros());
            portfolio.Evaluate(_bank, USD)
                .Should()
                .Be(17d.Dollars());
        }

        [Fact(DisplayName = "1 USD + 1100 KRW = 2200 KRW")]
        public void AddDollarsAndKoreanWons()
        {
            var portfolio = 1d.Dollars().AddToPortfolio(1100d.KoreanWons());
            portfolio.Evaluate(_bank, KRW)
                .Should()
                .Be(2200d.KoreanWons());
        }

        [Fact(DisplayName = "Throws a MissingExchangeRatesException case of missing exchange rates")]
        public void AddWithMissingExchangeRatesShouldThrowAMissingExchangeRatesException()
        {
            var portfolio = 1d.Dollars()
                .AddToPortfolio(1d.Euros())
                .AddToPortfolio(1d.KoreanWons());

            Action evaluate = () => portfolio.Evaluate(_bank, KRW);

            evaluate.Should()
                .Throw<MissingExchangeRatesException>()
                .WithMessage("Missing exchange rate(s): [EUR->KRW]");
        }
    }
}