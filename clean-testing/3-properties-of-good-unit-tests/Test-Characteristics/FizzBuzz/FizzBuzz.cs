namespace FizzBuzz
{
    public class FizzBuzz
    {
        private readonly int _lowerBound;
        private readonly int _upperBound;

        private FizzBuzz(int lowerBound, int upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public static FizzBuzz New(int lowerBound = 1, int upperBound = 100)
            => IsValidRange(lowerBound, upperBound)
                ? new FizzBuzz(lowerBound, upperBound)
                : throw new InvalidRangeException(lowerBound, upperBound);

        private static bool IsValidRange(int lowerBound, int upperBound)
            => lowerBound > 0 && upperBound > 0 && lowerBound < upperBound;

        public string Convert(int value)
            => IsInRange(value)
                ? ConvertSafely(value)
                : throw new NotInRangeException(value, _lowerBound, _upperBound);

        private bool IsInRange(int value)
            => value >= _lowerBound && value <= _upperBound;

        private string ConvertSafely(int value) =>
            (value % 3, value % 5) switch
            {
                (0, 0) => "FizzBuzz",
                (0, _) => "Fizz",
                (_, 0) => "Buzz",
                _ => $"Simple Value : {value}"
            };
    }
}