
public class CarFactory : AbstractFactory
{
    public override void CreateAgent()
    {
        var agent = Instantiate(_agentPrefab, _spawnLocation.position, _spawnLocation.rotation);
        agent.GetComponent<CarAgent>().Navigate(_agentDestination);
    }
}
