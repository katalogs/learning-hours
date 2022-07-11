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

        protected AccountServiceShould()
            => _accountService = new AccountService(
                new UserRepositoryFake(),
                _twitterServiceMockBuilder.Object,
                _businessLoggerMock.Object
            );

        public class Fail : AccountServiceShould
        {
            [Fact]
            public async Task When_User_Is_Unknown()
                => await AssertFailureAsync<UnknownUserException>(UnknownUserId,
                    "Unknown user 376510ae-4e7e-11ea-b77f-2e728ce88121");

            [Fact]
            public async Task When_Twitter_Registration_Failed()
                => await AssertFailureAsync<TwitterRegistrationFailedException>(ExistingUser.Id,
                    "Not able to register rick@green.com on twitter");

            [Fact]
            public async Task When_Twitter_Authentication_Failed()
            {
                _twitterServiceMockBuilder
                    .RegisterForUser(ExistingUser);

                await AssertFailureAsync<TwitterAuthenticationFailedException>(
                    ExistingUser.Id,
                    "Not able to authenticate rick@green.com on twitter"
                );
            }

            [Fact]
            public async Task When_Tweet_Failed()
            {
                _twitterServiceMockBuilder
                    .RegisterForUser(ExistingUser)
                    .AuthenticationForUser(ExistingUser, TwitterToken);

                await AssertFailureAsync<TweetFailedException>(
                    ExistingUser.Id,
                    "Not able to tweet with token A Twitter Token"
                );
            }

            [Fact]
            public async Task When_An_Unknown_Exception_Occured()
            {
                _twitterServiceMockBuilder
                    .RegisterForUser(ExistingUser)
                    .AuthenticationForUser(ExistingUser, TwitterToken)
                    .Tweet(TwitterToken, TweetUrl);

                _businessLoggerMock
                    .Setup(_ => _.LogSuccessAsync(ExistingUser.Id))
                    .Throws<InvalidOperationException>();


                await AssertFailureAsync<InvalidOperationException>(
                    ExistingUser.Id,
                    "Operation is not valid due to the current state of the object."
                );

                _businessLoggerMock
                    .Verify(_ => _.LogFailureAsync(ExistingUser.Id, It.IsAny<Exception>()), Once);
            }

            private async Task AssertFailureAsync<TException>(Guid userId, string expectedErrorMessage)
                where TException : Exception
            {
                var registerAsync = () => _accountService.RegisterAsync(userId);
                await registerAsync
                    .Should()
                    .ThrowAsync<TException>()
                    .WithMessage(expectedErrorMessage);
            }
        }

        public class Succeed : AccountServiceShould
        {
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
}