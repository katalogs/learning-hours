using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TestDoubles;

public class Fake
{
    private readonly ITestOutputHelper _output;

    public Fake(ITestOutputHelper output) => _output = output;

    [Fact]
    public void Should_Notify_Twice_When_Receiving_A_Scenario_And_Having_Two_Clients()
    {
        var registeredUsers = new List<Client>
        {
            new("Cliff Booth", "cliff.booth@double.com"),
            new("Rick Dalton", "rick.dalton@double.com")
        };

        var notifier = new FakeNotifier(text => _output.WriteLine(text));
        var scriptEventHandler = new ScenarioReceivedEventHandler(notifier, registeredUsers);

        scriptEventHandler.Handle(new ScenarioReceived("The 14 fists of McCluskey"));

        ((TestOutputHelper) _output).Output
            .Should()
            .Contain(
                "Hello : Cliff Booth, I have just received a new scenario called 'The 14 fists of McCluskey' !!!")
            .And
            .Contain(
                "Hello : Rick Dalton, I have just received a new scenario called 'The 14 fists of McCluskey' !!!");
    }

    private record ScenarioReceived(string Title);

    private class ScenarioReceivedEventHandler
    {
        private readonly INotifier _notifier;
        private readonly IEnumerable<Client> _registeredUsers;

        public ScenarioReceivedEventHandler(INotifier notifier, IEnumerable<Client> registeredUsers)
        {
            _notifier = notifier;
            _registeredUsers = registeredUsers;
        }

        public void Handle(ScenarioReceived scenarioReceived) =>
            _registeredUsers
                .ToList()
                .ForEach(user => _notifier.Notify(user, scenarioReceived));
    }

    private class FakeNotifier : INotifier
    {
        private readonly Action<string> _console;

        public FakeNotifier(Action<string> console) => _console = console;

        public void Notify(Client client, ScenarioReceived scenarioReceived) =>
            _console(
                $"Hello : {client.Name}, I have just received a new scenario called '{scenarioReceived.Title}' !!!");
    }

    private interface INotifier
    {
        void Notify(Client client, ScenarioReceived scenarioReceived);
    }

    private record Client(string Name, string Email);
}