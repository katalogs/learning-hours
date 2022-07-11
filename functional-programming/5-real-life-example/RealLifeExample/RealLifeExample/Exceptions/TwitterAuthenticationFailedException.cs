namespace RealLifeExample.Exceptions
{
    public class TwitterAuthenticationFailedException : Exception
    {
        public TwitterAuthenticationFailedException(User user) : base(
            $"Not able to authenticate {user.Email} on twitter")
        {
        }
    }
}