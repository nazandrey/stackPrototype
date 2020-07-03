using UnityEngine;

public class StartGameHandler : IStartGameHandler
{
    private IBlockSpawner _spawner;

    public StartGameHandler(IBlockSpawner spawner)
    {
        _spawner = spawner;
    }
    
    public void StartGame()
    {
        _spawner.Spawn();
    }
}
