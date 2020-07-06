using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BlockSpawnStarter : MonoBehaviour, IBlockSpawnStarter
{
    [SerializeField] 
    private List<BlockSpawner> spawners;

    private ITower _tower;
    
    [Inject]
    public void Construct(ITower tower)
    {
        _tower = tower;
        _tower.BlockAttached += OnBlockAttached;
    }

    private void OnDestroy()
    {
        _tower.BlockAttached -= OnBlockAttached;
    }

    private void OnBlockAttached(object sender, EventArgs e)
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        var spawnerIndex = Random.Range(0, spawners.Count);
        var chosenSpawner = spawners[spawnerIndex];
        chosenSpawner.Spawn();
    }
}
