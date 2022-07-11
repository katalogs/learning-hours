namespace RealLifeExample.Exceptions
{
    public sealed class TweetFailedException : Exception
    {
        public TweetFailedException(string token) : base($"Not able to tweet with token {token}")
        {
        }
    }
}