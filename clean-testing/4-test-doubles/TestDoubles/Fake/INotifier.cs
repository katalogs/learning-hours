namespace TestDoubles.Fake
{
    public interface INotifier
    {
        void Notify(Client client, ScenarioReceived scenarioReceived);
    }
}