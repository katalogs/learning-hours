using System;
using FluentAssertions;
using Moq;
using Xunit;
using static Moq.Times;

namespace RealLifeExample.Tests
{
    public class AccountServiceShould
    {
        private static readonly User ExistingUser = UsersForTests.Rick;
        private static readonly Guid UnknownUserId = Guid.Parse("376510ae-4e7e-11ea-b77f-2e728ce88121");

        private const string TwitterToken = "A Twitter Token";
        private const string TweetUrl = "TweetUrl";

        private readonly AccountService _accountService;
        private readonly Mock<ITwitterService> _twitterServiceMock = new();
        private readonly Mock<IBusinessLogger> _businessLoggerMock = new();

        public AccountServiceShould()
            => _accountService = new AccountService(
                new UserRepositoryFake(),
                _twitterServiceMock.Object,
                _businessLoggerMock.Object
            );

        public class ReturnNull : AccountServiceShould
        {
            [Fact]
            public void When_User_Is_Unknown() =>
                _accountService
                    .Register(UnknownUserId)
                    .Should()
                    .BeNull();

            [Fact]
            public void When_Twitter_Registration_Failed() =>
                _accountService
                    .Register(ExistingUser.Id)
                    .Should()
                    .BeNull();

            [Fact]
            public void When_Twitter_Authentication_Failed()
            {
                SetupRegisterForUser(ExistingUser);

                _accountService
                    .Register(ExistingUser.Id)
                    .Should()
                    .BeNull();
            }

            [Fact]
            public void When_Twitter_Tweet_Failed()
            {
                SetupRegisterForUser(ExistingUser);
                SetupAuthenticationForUser(ExistingUser);

                _accountService
                    .Register(ExistingUser.Id)
                    .Should()
                    .BeNull();
            }

            [Fact]
            public void When_Logging_Failed()
            {
                SetupRegisterForUser(ExistingUser);
                SetupAuthenticationForUser(ExistingUser);
                SetupTweet();

                _businessLoggerMock
                    .Setup(_ => _.LogSuccessRegister(ExistingUser.Id))
                    .Throws<InvalidOperationException>();

                _accountService
                    .Register(ExistingUser.Id)
                    .Should()
                    .BeNull();

                _businessLoggerMock
                    .Verify(_ => _.LogFailureRegister(ExistingUser.Id, It.IsAny<Exception>()), Once);
            }
        }

        [Fact]
        public void Return_A_New_Tweet_Url()
        {
            SetupRegisterForUser(ExistingUser);
            SetupAuthenticationForUser(ExistingUser);
            SetupTweet();

            _accountService
                .Register(ExistingUser.Id)
                .Should()
                .Be(TweetUrl);

            _businessLoggerMock
                .Verify(_ => _.Log("Twitter account updated"), Once);

            _businessLoggerMock
                .Verify(_ => _.LogSuccessRegister(ExistingUser.Id));
        }

        private void SetupRegisterForUser(User user) =>
            _twitterServiceMock
                .Setup(t => t.Register(user.Email, user.Name))
                .Returns("AccountId");

        private void SetupAuthenticationForUser(User user) =>
            _twitterServiceMock
                .Setup(t => t.Authenticate(user.Email, user.Password))
                .Returns(TwitterToken);

        private void SetupTweet() =>
            _twitterServiceMock
                .Setup(t => t.Tweet(TwitterToken, It.IsAny<string>()))
                .Returns(TweetUrl);
    }
}