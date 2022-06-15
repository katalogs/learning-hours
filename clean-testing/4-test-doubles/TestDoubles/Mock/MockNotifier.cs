using System.Collections.Generic;
using System.Linq;

namespace TestDoubles.Mock
{
    public class MockNotifier : INotifier
    {
        private readonly List<(Client, ScenarioReceived)> _notifications = new();

        public void Notify(Client client, ScenarioReceived scenarioReceived) =>
            _notifications.Add((client, scenarioReceived));

        public bool HasBeenCalledForAll(IEnumerable<Client> clients, ScenarioReceived @event) =>
            clients.ToList()
                .TrueForAll(client => _notifications.Contains((client, @event)));
    }
}