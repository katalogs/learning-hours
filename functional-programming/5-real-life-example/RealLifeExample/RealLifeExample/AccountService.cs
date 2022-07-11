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

        public async Task<string> RegisterAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.FindByIdAsync(id);

                if (user == null)
                {
                    throw new UnknownUserException(id);
                }

                var accountId = await _twitterService.RegisterAsync(user.Email, user.Name);

                if (accountId == null)
                {
                    throw new TwitterRegistrationFailedException(user);
                }

                var twitterToken = await _twitterService.AuthenticateAsync(user.Email, user.Password);

                if (twitterToken == null)
                {
                    throw new TwitterAuthenticationFailedException(user);
                }

                var tweetUrl = await _twitterService.TweetAsync(twitterToken, "Hello I am " + user.Name);

                if (tweetUrl == null)
                {
                    throw new TweetFailedException(twitterToken);
                }

                await UpdateTwitterAccountIdAsync(id, accountId);
                await _businessLogger.LogSuccessAsync(id);

                return await Task.FromResult(tweetUrl);
            }
            catch (Exception ex)
            {
                await _businessLogger.LogFailureAsync(id, ex);
                throw;
            }
        }

        private async Task UpdateTwitterAccountIdAsync(Guid id, string twitterAccountId)
        {
            await _businessLogger.LogAsync("Twitter account updated");
        }
    }
}