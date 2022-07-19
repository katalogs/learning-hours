using System.Collections.Immutable;

namespace money_problem.Domain
{
    public record Bank
    {
        private readonly ImmutableDictionary<string, double> _exchangeRates;

        private Bank(ImmutableDictionary<string, double> exchangeRates) => _exchangeRates = exchangeRates;

        public static Bank WithExchangeRate(Currency from, Currency to, double rate) =>
            new Bank(new Dictionary<string, double>().ToImmutableDictionary())
                .AddExchangeRate(from, to, rate);

        public Bank AddExchangeRate(Currency from, Currency to, double rate) =>
            new(_exchangeRates.SetItem(KeyFor(from, to), rate));

        private static string KeyFor(Currency from, Currency to) => $"{from}->{to}";

        public Money Convert(Money money, Currency toCurrency) =>
            CanConvert(money.Currency, toCurrency)
                ? ConvertSafely(money, toCurrency)
                : throw new MissingExchangeRateException(money.Currency, toCurrency);

        private Money ConvertSafely(Money money, Currency currency) =>
            currency == money.Currency
                ? money
                : new Money(money.Amount * _exchangeRates[KeyFor(money.Currency, currency)], currency);

        private bool CanConvert(Currency from, Currency to) =>
            from == to || _exchangeRates.ContainsKey(KeyFor(from, to));
    }
}