using LanguageExt;
using LanguageExt.Common;

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

        public EitherAsync<Error, string> RegisterAsync(Guid id) =>
            RetrieveUserDetailsAsync(id)
                .Bind(RegisterAccountOnTwitterAsync)
                .Bind(AuthenticateOnTwitterAsync)
                .Bind(TweetAsync)
                .Bind(UpdateTwitterAccountIdAsync)
                .Bind(LogSuccessAsync)
                .Map(context => context.Url)
                .DoOnFailure(async failure => await _businessLogger.LogFailureAsync(id, failure));

        private EitherAsync<Error, RegistrationContext> RetrieveUserDetailsAsync(Guid id)
            => _userRepository.FindByIdAsync(id)
                .Map(user => new RegistrationContext(user.Id, user.Email, user.Name, user.Password));

        private EitherAsync<Error, RegistrationContext> RegisterAccountOnTwitterAsync(RegistrationContext context)
            => _twitterService.RegisterAsync(context.Email, context.Name)
                .Map(accountId => context with {AccountId = accountId});

        private EitherAsync<Error, RegistrationContext> AuthenticateOnTwitterAsync(RegistrationContext context)
            => _twitterService.AuthenticateAsync(context.Email, context.Password)
                .Map(twitterToken => context with {Token = twitterToken});

        private EitherAsync<Error, RegistrationContext> TweetAsync(RegistrationContext context)
            => _twitterService.TweetAsync(context.Token, "Hello I am " + context.Name)
                .Map(tweetUrl => context with {Url = tweetUrl});

        private EitherAsync<Error, RegistrationContext> UpdateTwitterAccountIdAsync(RegistrationContext context)
            => _businessLogger.LogAsync("Twitter account updated")
                .Map(_ => context)
                .ToEither();

        private EitherAsync<Error, RegistrationContext> LogSuccessAsync(RegistrationContext context)
            => _businessLogger.LogSuccessAsync(context.Id)
                .Map(_ => context)
                .ToEither();
    }
}