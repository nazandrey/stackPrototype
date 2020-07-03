using UnityEngine;

public class StartGameHandler : IStartGameHandler
{
    private IBlockSpawnStarter _spawnStarter;

    public StartGameHandler(IBlockSpawnStarter spawnStarter)
    {
        _spawnStarter = spawnStarter;
    }
    
    public void StartGame()
    {
        _spawnStarter.StartSpawn();
    }
}
