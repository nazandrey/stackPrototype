using System;
using UnityEngine;
using Zenject;

public class BlockSpawner : MonoBehaviour, IBlockSpawner
{
    [SerializeField] 
    private Transform finishPoint;

    private Block.Factory _blockFactory;

    [Inject]
    public void Construct( Block.Factory blockFactory)
    {
        _blockFactory = blockFactory;
    }

    public void Spawn()
    {
        var block = _blockFactory.Create(transform.position, finishPoint.position);
        block.transform.SetParent(transform, true);
    }
}