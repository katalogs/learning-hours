namespace RealLifeExample
{
    public class TwitterService
    {
        public string Register(string email, string name) => "TwitterAccountId";
        public string Authenticate(string email, string password) => "ATwitterToken";
        public string Tweet(string token, string message) => "TweetUrl";
    }
}