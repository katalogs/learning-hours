namespace RealLifeExample
{
    public class AccountService
    {
        private readonly IBusinessLogger _businessLogger;
        private readonly TwitterService _twitterService;
        private readonly UserService _userService;

        public AccountService(
            UserService userService,
            TwitterService twitterService,
            IBusinessLogger businessLogger)
        {
            _userService = userService;
            _twitterService = twitterService;
            _businessLogger = businessLogger;
        }

        public string Register(Guid id)
        {
            try
            {
                var user = _userService.FindById(id);

                if (user == null) return null;

                var accountId = _twitterService.Register(user.Email, user.Name);

                if (accountId == null) return null;

                var twitterToken = _twitterService.Authenticate(user.Email, user.Password);

                if (twitterToken == null) return null;

                var tweetUrl = _twitterService.Tweet(twitterToken, "Hello I am " + user.Name);

                if (tweetUrl == null) return null;

                _userService.UpdateTwitterAccountId(id, accountId);
                _businessLogger.LogSuccessRegister(id);

                return tweetUrl;
            }
            catch (Exception ex)
            {
                _businessLogger.LogFailureRegister(id, ex);

                return null;
            }
        }
    }
}
