using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace TestDoubles.Mock
{
    public class MockTests
    {
        [Fact]
        public void Should_Notify_Twice_When_Receiving_A_Scenario_And_Having_Two_Clients()
        {
            var clients = new List<Client>
            {
                new("Cliff Booth", "cliff.booth@double.com"),
                new("Rick Dalton", "rick.dalton@double.com")
            };

            var notifier = new MockNotifier();
            var scriptEventHandler = new ScenarioReceivedEventHandler(notifier, clients);
            var @event = new ScenarioReceived("The 14 fists of McCluskey");

            scriptEventHandler.Handle(@event);

            notifier
                .HasBeenCalledForAll(clients, @event)
                .Should()
                .BeTrue();
        }
    }
}