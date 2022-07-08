using RealLifeExample.Exceptions;

namespace RealLifeExample
{
    public class AccountService
    {
        private readonly IBusinessLogger _businessLogger;
        private readonly IUserRepository _userRepository;
        private readonly ITwitterService _twitterService;

        public AccountService(
            IUserRepository userRepository,
            ITwitterService twitterService,
            IBusinessLogger businessLogger)
        {
            _userRepository = userRepository;
            _twitterService = twitterService;
            _businessLogger = businessLogger;
        }

        public string Register(Guid id)
        {
            try
            {
                var user = _userRepository.FindById(id);

                if (user == null)
                {
                    throw new UnknownUserException(id);
                }

                var accountId = _twitterService.Register(user.Email, user.Name);

                if (accountId == null)
                {
                    throw new TwitterRegistrationFailedException(user);
                }

                var twitterToken = _twitterService.Authenticate(user.Email, user.Password);

                if (twitterToken == null)
                {
                    throw new TwitterAuthenticationFailedException(user);
                }

                var tweetUrl = _twitterService.Tweet(twitterToken, "Hello I am " + user.Name);

                if (tweetUrl == null)
                {
                    throw new TweetFailedException(twitterToken);
                }

                UpdateTwitterAccountId(id, accountId);
                _businessLogger.LogSuccessRegister(id);

                return tweetUrl;
            }
            catch (Exception ex)
            {
                _businessLogger.LogFailureRegister(id, ex);
                throw;
            }
        }

        private void UpdateTwitterAccountId(Guid id, string twitterAccountId)
        {
            _businessLogger.Log("Twitter account updated");
        }
    }
}