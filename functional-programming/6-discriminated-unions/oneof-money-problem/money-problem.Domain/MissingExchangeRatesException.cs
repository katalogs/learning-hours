using System.Runtime.Serialization;

namespace money_problem.Domain
{
    [Serializable]
    public class MissingExchangeRatesException : Exception
    {
        protected MissingExchangeRatesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public MissingExchangeRatesException(IEnumerable<MissingExchangeRateException> missingExchangeRateExceptions)
            : base(
                $"Missing exchange rate(s): [{string.Join(",", missingExchangeRateExceptions.Select(_ => _.Message))}]")
        {
        }
    }
}