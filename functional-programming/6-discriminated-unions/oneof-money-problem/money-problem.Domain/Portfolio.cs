namespace money_problem.Domain
{
    public record Portfolio(params Money[] Moneys)
    {
        public Money Evaluate(
            Bank bank,
            Currency toCurrency)
        {
            var missingExchangeRates = new List<MissingExchangeRateException>();
            var convertedMoneys = new List<Money>();

            foreach (var money in Moneys)
            {
                try
                {
                    convertedMoneys.Add(bank.Convert(money, toCurrency));
                }
                catch (MissingExchangeRateException e)
                {
                    missingExchangeRates.Add(e);
                }
            }

            return !missingExchangeRates.Any()
                ? new Money(convertedMoneys.Aggregate(0d, (acc, money) => acc + money.Amount), toCurrency)
                : throw new MissingExchangeRatesException(missingExchangeRates);
        }
    }
}