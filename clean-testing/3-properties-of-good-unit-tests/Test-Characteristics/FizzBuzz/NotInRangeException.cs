namespace FizzBuzz
{
    public class NotInRangeException : Exception
    {
        public NotInRangeException(int value, int lowerBound, int upperBound)
            : base($"The provided value : {value} is not supported by the range [{lowerBound},{upperBound}]")
        {
        }
    }
}