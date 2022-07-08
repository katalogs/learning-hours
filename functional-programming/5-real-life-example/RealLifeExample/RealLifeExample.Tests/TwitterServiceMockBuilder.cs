using System;
using Moq;

namespace RealLifeExample.Tests;

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
            _ => _.Setup(t => t.Register(user.Email, user.Name))
                .Returns("AccountId")
        );

    public TwitterServiceMockBuilder AuthenticationForUser(User user, string twitterToken) =>
        SetupTwitterMock(
            _ => _.Setup(t => t.Authenticate(user.Email, user.Password))
                .Returns(twitterToken)
        );

    public TwitterServiceMockBuilder Tweet(string twitterToken, string returnedTweetUrl) =>
        SetupTwitterMock(
            _ => _.Setup(t => t.Tweet(twitterToken, It.IsAny<string>()))
                .Returns(returnedTweetUrl)
        );
}