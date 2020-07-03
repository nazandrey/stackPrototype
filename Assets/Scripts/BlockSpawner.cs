using System;
using UnityEngine;
using Zenject;

public class BlockSpawner : MonoBehaviour, IBlockSpawner
{
    [SerializeField] 
    private Transform finishPoint;

    private ITower _tower;
    private Block.Factory _blockFactory;

    [Inject]
    public void Construct(ITower tower, Block.Factory blockFactory)
    {
        _tower = tower;
        _blockFactory = blockFactory;
    }
    
    private void Awake()
    {
        _tower.BlockAttached += OnBlockAttached;
    }

    private void OnBlockAttached(object sender, EventArgs e)
    {
        Spawn();
    }

    public void Spawn()
    {
        var block = _blockFactory.Create(transform.position, finishPoint.position);
        block.transform.SetParent(transform, true);
    }
}