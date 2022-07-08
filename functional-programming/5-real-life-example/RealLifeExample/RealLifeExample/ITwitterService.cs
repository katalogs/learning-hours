namespace RealLifeExample
{
    public interface ITwitterService
    {
        string Register(string email, string name);
        string Authenticate(string email, string password);
        string Tweet(string token, string message);
    }
}