using System;
using LanguageExt.Common;
using Moq;
using static LanguageExt.Prelude;

namespace RealLifeExample.Tests
{
    public class TwitterServiceMockBuilder
    {
        private readonly Mock<ITwitterService> _mock;
        public ITwitterService Object => _mock.Object;

        private TwitterServiceMockBuilder() => _mock = new Mock<ITwitterService>();

        public static TwitterServiceMockBuilder NewTwitterService() => new();

        private TwitterServiceMockBuilder SetupTwitterMock(Action<Mock<ITwitterService>> setup)
        {
            setup(_mock);
            return this;
        }

        public TwitterServiceMockBuilder AllowRegistration(User user) =>
            SetupTwitterMock(
                _ => _.Setup(t => t.RegisterAsync(user.Email, user.Name))
                    .Returns(RightAsync<Error, string>("AccountId"))
            );

        public TwitterServiceMockBuilder DenyRegistration(User user) =>
            SetupTwitterMock(
                _ => _.Setup(t => t.RegisterAsync(user.Email, user.Name))
                    .Returns(LeftAsync<Error, string>(Error.New($"Not able to register {user.Email} on twitter")))
            );

        public TwitterServiceMockBuilder AllowAuthentication(User user, string twitterToken) =>
            SetupTwitterMock(
                _ => _.Setup(t => t.AuthenticateAsync(user.Email, user.Password))
                    .Returns(RightAsync<Error, string>(twitterToken))
            );

        public TwitterServiceMockBuilder DenyAuthentication(User user) =>
            SetupTwitterMock(
                _ => _.Setup(t => t.AuthenticateAsync(user.Email, user.Password))
                    .Returns(LeftAsync<Error, string>(Error.New($"Not able to authenticate {user.Email} on twitter")))
            );

        public TwitterServiceMockBuilder AllowTweet(string twitterToken, string returnedTweetUrl) =>
            SetupTwitterMock(
                _ => _.Setup(t => t.TweetAsync(twitterToken, It.IsAny<string>()))
                    .Returns(RightAsync<Error, string>(returnedTweetUrl))
            );

        public TwitterServiceMockBuilder DenyTweet(string twitterToken) =>
            SetupTwitterMock(
                _ => _.Setup(t => t.TweetAsync(twitterToken, It.IsAny<string>()))
                    .Returns(LeftAsync<Error, string>(Error.New($"Not able to tweet with token {twitterToken}")))
            );
    }
}