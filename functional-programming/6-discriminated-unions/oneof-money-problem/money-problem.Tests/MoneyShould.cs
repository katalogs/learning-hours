using FluentAssertions;
using money_problem.Domain;
using Xunit;

namespace money_problem.Tests
{
    public class MoneyShould
    {
        [Fact(DisplayName = "10 EUR x 2 = 20 EUR")]
        public void Multiply()
        {
            10d.Euros()
                .Times(2)
                .Should()
                .Be(20d.Euros());
        }

        [Fact(DisplayName = "4002 KRW / 4 = 1000.5 KRW")]
        public void Divide()
        {
            4002d.KoreanWons()
                .Divide(4)
                .Should()
                .Be(1000.5d.KoreanWons());
        }
    }
}