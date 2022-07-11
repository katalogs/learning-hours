using System;
using Moq;
using static System.Threading.Tasks.Task;

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

        public TwitterServiceMockBuilder RegisterForUser(User user) =>
            SetupTwitterMock(
                _ => _.Setup(t => t.RegisterAsync(user.Email, user.Name))
                    .Returns(FromResult("AccountId"))
            );

        public TwitterServiceMockBuilder AuthenticationForUser(User user, string twitterToken) =>
            SetupTwitterMock(
                _ => _.Setup(t => t.AuthenticateAsync(user.Email, user.Password))
                    .Returns(FromResult(twitterToken))
            );

        public TwitterServiceMockBuilder Tweet(string twitterToken, string returnedTweetUrl) =>
            SetupTwitterMock(
                _ => _.Setup(t => t.TweetAsync(twitterToken, It.IsAny<string>()))
                    .Returns(FromResult(returnedTweetUrl))
            );
    }
}