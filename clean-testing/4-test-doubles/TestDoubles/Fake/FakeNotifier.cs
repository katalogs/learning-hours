using System;

namespace TestDoubles.Fake
{
    public class FakeNotifier : INotifier
    {
        private readonly Action<string> _console;

        public FakeNotifier(Action<string> console) => _console = console;

        public void Notify(Client client, ScenarioReceived scenarioReceived) =>
            _console(
                $"Hello : {client.Name}, I have just received a new scenario called '{scenarioReceived.Title}' !!!");
    }
}