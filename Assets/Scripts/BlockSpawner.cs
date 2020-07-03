using System;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] 
    private Block blockPrefab;

    [SerializeField] 
    private Transform finishPoint;
    
    [SerializeField] 
    private Tower tower;

    [SerializeField] 
    private GameOverHandler gameOverHandler;

    private void Awake()
    {
        tower.BlockAttached += OnBlockAttached;
    }

    private void Start()
    {
        Spawn();
    }

    private void OnBlockAttached(object sender, EventArgs e)
    {
        Spawn();
    }

    private void Spawn()
    {
        var block = Instantiate(blockPrefab, transform, false);
        block.Construct(gameOverHandler, transform.position, finishPoint.position);
    }
}