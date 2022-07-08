using System;
using FluentAssertions;
using Moq;
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
                _twitterServiceMockBuilder
                    .RegisterForUser(ExistingUser);

                _accountService
                    .Register(ExistingUser.Id)
                    .Should()
                    .BeNull();
            }

            [Fact]
            public void When_Twitter_Tweet_Failed()
            {
                _twitterServiceMockBuilder
                    .RegisterForUser(ExistingUser)
                    .AuthenticationForUser(ExistingUser, TwitterToken);

                _accountService
                    .Register(ExistingUser.Id)
                    .Should()
                    .BeNull();
            }

            [Fact]
            public void When_Logging_Failed()
            {
                _twitterServiceMockBuilder
                    .RegisterForUser(ExistingUser)
                    .AuthenticationForUser(ExistingUser, TwitterToken)
                    .Tweet(TwitterToken, TweetUrl);

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
            _twitterServiceMockBuilder
                .RegisterForUser(ExistingUser)
                .AuthenticationForUser(ExistingUser, TwitterToken)
                .Tweet(TwitterToken, TweetUrl);

            _accountService
                .Register(ExistingUser.Id)
                .Should()
                .Be(TweetUrl);

            _businessLoggerMock
                .Verify(_ => _.Log("Twitter account updated"), Once);

            _businessLoggerMock
                .Verify(_ => _.LogSuccessRegister(ExistingUser.Id));
        }
    }
}