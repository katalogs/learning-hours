using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Xunit;

namespace TestDoubles.Fake
{
    public class Fake
    {
        [Fact]
        public void Should_Notify_Twice_When_Receiving_A_Scenario_And_Having_Two_Clients()
        {
            using var logs = new StringWriter();
            var registeredUsers = new List<Client>
            {
                new("Cliff Booth", "cliff.booth@double.com"),
                new("Rick Dalton", "rick.dalton@double.com")
            };

            var notifier = new FakeNotifier(text => logs.WriteLine(text));
            var scriptEventHandler = new ScenarioReceivedEventHandler(notifier, registeredUsers);

            scriptEventHandler.Handle(new ScenarioReceived("The 14 fists of McCluskey"));

            logs.ToString()
                .Should()
                .Contain(
                    "Hello : Cliff Booth, I have just received a new scenario called 'The 14 fists of McCluskey' !!!")
                .And
                .Contain(
                    "Hello : Rick Dalton, I have just received a new scenario called 'The 14 fists of McCluskey' !!!");
        }
    }
}