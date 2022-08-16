namespace FizzBuzz
{
    public class InvalidRangeException : Exception
    {
        public InvalidRangeException(int lowerBound, int upperBound)
            : base($"Invalid range for [{lowerBound},{upperBound}]")
        {
        }
    }
}