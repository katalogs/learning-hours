using System.Runtime.Serialization;

namespace money_problem.Domain
{
    [Serializable]
    public class MissingExchangeRateException : Exception
    {
        protected MissingExchangeRateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public MissingExchangeRateException(Currency from, Currency to)
            : base($"{from}->{to}")
        {
        }
    }
}