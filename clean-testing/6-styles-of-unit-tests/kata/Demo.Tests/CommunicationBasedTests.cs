using Demo.Greeter;
using Moq;
using Xunit;

namespace Demo.Tests
{

    public class CommunicationBasedTests
    {
        [Fact]
        public void Greet_A_User_Should_Send_An_Email_To_It()
        {
            const string email = "john.doe@email.com";
            var emailGatewayMock = new Mock<IEmailGateway>();

            // Substitute collaborators with Test Double
            var sut = new Controller(emailGatewayMock.Object);

            sut.GreetUser(email);

            // Verify that the SUT calls collaborators correctly
            emailGatewayMock.Verify(e => e.SendGreetingsEmail(email), Times.Once);
        }
    }

}