using System;
using UnityEngine;
using Zenject;

public class BlockSpawner : MonoBehaviour, IBlockSpawner
{
    [SerializeField] 
    private Transform finishPoint;

    private Block.Factory _blockFactory;
    private IBlockSizeSetter _blockSizeSetter;

    [Inject]
    public void Construct(Block.Factory blockFactory, IBlockSizeSetter blockSizeSetter)
    {
        _blockFactory = blockFactory;
        _blockSizeSetter = blockSizeSetter;
    }

    public void Spawn()
    {
        var block = _blockFactory.Create(transform.position, finishPoint.position);
        block.transform.SetParent(transform, true);
        if (_blockSizeSetter.ShouldApplySize())
            _blockSizeSetter.ApplySize(block.transform);
    }
}