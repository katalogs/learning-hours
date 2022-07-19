namespace money_problem.Domain
{
    public static class DomainExtensions
    {
        public static Money Dollars(this double amount) => new(amount, Currency.USD);
        public static Money Euros(this double amount) => new(amount, Currency.EUR);
        public static Money KoreanWons(this double amount) => new(amount, Currency.KRW);
        public static Portfolio AddToPortfolio(this Money money1, Money money2) => new(money1, money2);

        public static Portfolio AddToPortfolio(this Portfolio portfolio, Money money) =>
            new(portfolio.Moneys.Append(money).ToArray());
    }
}