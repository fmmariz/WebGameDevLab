

public class BoatFactory : AbstractFactory
{
    public override void CreateAgent()
    {
        var agent = Instantiate(_agentPrefab, _spawnLocation.position, _spawnLocation.rotation);
        agent.GetComponent<BoatAgent>().Navigate(_agentDestination);
    }
}
