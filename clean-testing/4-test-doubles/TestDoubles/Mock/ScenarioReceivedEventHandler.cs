using System.Collections.Generic;
using System.Linq;

namespace TestDoubles.Mock
{
    public class ScenarioReceivedEventHandler
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
}