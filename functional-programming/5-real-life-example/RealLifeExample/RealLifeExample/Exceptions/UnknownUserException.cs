namespace RealLifeExample.Exceptions
{
    public class UnknownUserException : Exception
    {
        public UnknownUserException(Guid id) : base($"Unknown user {id}")
        {
        }
    }
}