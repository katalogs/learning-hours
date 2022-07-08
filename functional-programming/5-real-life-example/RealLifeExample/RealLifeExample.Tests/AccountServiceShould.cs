using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RealLifeExample.Exceptions;
using Xunit;
using static Moq.Times;
using static RealLifeExample.Tests.TwitterServiceMockBuilder;

namespace RealLifeExample.Tests
{
    public class AccountServiceShould
    {
        private static readonly User ExistingUser = UsersForTests.Rick;
        private static readonly Guid UnknownUserId = Guid.Parse("376510ae-4e7e-11ea-b77f-2e728ce88121");

        private const string TwitterToken = "A Twitter Token";
        private const string TweetUrl = "TweetUrl";

        private readonly AccountService _accountService;
        private readonly TwitterServiceMockBuilder _twitterServiceMockBuilder = NewTwitterService();
        private readonly Mock<IBusinessLogger> _businessLoggerMock = new();

        public AccountServiceShould()
            => _accountService = new AccountService(
                new UserRepositoryFake(),
                _twitterServiceMockBuilder.Object,
                _businessLoggerMock.Object
            );

        public class Fail : AccountServiceShould
        {
            [Fact]
            public void When_User_Is_Unknown() =>
                _accountService
                    .Invoking(_ => _.RegisterAsync(UnknownUserId))
                    .Should()
                    .ThrowAsync<UnknownUserException>();

            [Fact]
            public void When_Twitter_Registration_Failed() =>
                _accountService
                    .Invoking(_ => _.RegisterAsync(ExistingUser.Id))
                    .Should()
                    .ThrowAsync<TwitterRegistrationFailedException>()
                    .WithMessage("Not able to register rick@green.com on twitter");

            [Fact]
            public void When_Twitter_Authentication_Failed()
            {
                _twitterServiceMockBuilder
                    .RegisterForUser(ExistingUser);

                _accountService
                    .Invoking(_ => _.RegisterAsync(ExistingUser.Id))
                    .Should()
                    .ThrowAsync<TwitterAuthenticationFailedException>()
                    .WithMessage("Not able to authenticate rick@green.com on twitter");
            }

            [Fact]
            public void When_Tweet_Failed()
            {
                _twitterServiceMockBuilder
                    .RegisterForUser(ExistingUser)
                    .AuthenticationForUser(ExistingUser, TwitterToken);

                _accountService
                    .Invoking(_ => _.RegisterAsync(ExistingUser.Id))
                    .Should()
                    .ThrowAsync<TweetFailedException>()
                    .WithMessage("Not able to tweet with token A Twitter Token");
            }

            [Fact]
            public void When_An_Unknown_Exception_Occured()
            {
                _twitterServiceMockBuilder
                    .RegisterForUser(ExistingUser)
                    .AuthenticationForUser(ExistingUser, TwitterToken)
                    .Tweet(TwitterToken, TweetUrl);

                _businessLoggerMock
                    .Setup(_ => _.LogSuccessAsync(ExistingUser.Id))
                    .Throws<InvalidOperationException>();

                _accountService
                    .Invoking(_ => _.RegisterAsync(ExistingUser.Id))
                    .Should()
                    .ThrowAsync<InvalidOperationException>();

                _businessLoggerMock
                    .Verify(_ => _.LogFailureAsync(ExistingUser.Id, It.IsAny<Exception>()), Once);
            }
        }

        [Fact]
        public async Task Return_A_New_Tweet_Url()
        {
            _twitterServiceMockBuilder
                .RegisterForUser(ExistingUser)
                .AuthenticationForUser(ExistingUser, TwitterToken)
                .Tweet(TwitterToken, TweetUrl);

            (await _accountService.RegisterAsync(ExistingUser.Id))
                .Should()
                .Be(TweetUrl);

            _businessLoggerMock
                .Verify(_ => _.LogAsync("Twitter account updated"), Once);

            _businessLoggerMock
                .Verify(_ => _.LogSuccessAsync(ExistingUser.Id));
        }
    }
}