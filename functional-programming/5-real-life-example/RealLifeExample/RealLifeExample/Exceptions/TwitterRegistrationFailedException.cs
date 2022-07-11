namespace RealLifeExample.Exceptions
{
    public class TwitterRegistrationFailedException : Exception
    {
        public TwitterRegistrationFailedException(User user) : base($"Not able to register {user.Email} on twitter")
        {
        }
    }
}