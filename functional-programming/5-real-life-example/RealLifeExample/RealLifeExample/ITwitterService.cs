namespace RealLifeExample
{
    public interface ITwitterService
    {
        Task<string> RegisterAsync(string email, string name);
        Task<string> AuthenticateAsync(string email, string password);
        Task<string> TweetAsync(string token, string message);
    }
}