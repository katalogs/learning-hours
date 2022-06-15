namespace TestDoubles.Mock
{
    public interface INotifier
    {
        void Notify(Client client, ScenarioReceived scenarioReceived);
    }
}