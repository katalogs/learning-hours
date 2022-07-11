using LanguageExt;
using LanguageExt.Common;

namespace RealLifeExample
{
    public interface ITwitterService
    {
        EitherAsync<Error, string> RegisterAsync(string email, string name);
        EitherAsync<Error, string> AuthenticateAsync(string email, string password);
        EitherAsync<Error, string> TweetAsync(string token, string message);
    }
}