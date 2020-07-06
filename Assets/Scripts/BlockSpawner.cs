using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

[assembly: InternalsVisibleTo("Tests.EditMode")]
public class BlockSpawner : MonoBehaviour, IBlockSpawner
{
    [SerializeField] 
    internal Transform finishPoint;

    private Block.Factory _blockFactory;
    private IBlockSizeSetter _blockSizeSetter;
    private IBlockSlicer _blockSlicer;

    [Inject]
    public void Construct(Block.Factory blockFactory, IBlockSizeSetter blockSizeSetter, IBlockSlicer blockSlicer)
    {
        _blockSlicer = blockSlicer;
        _blockSlicer.BlockPositionChanged += OnBlockPositionChanged;
        _blockFactory = blockFactory;
        _blockSizeSetter = blockSizeSetter;
    }

    private void OnDestroy()
    {
        _blockSlicer.BlockPositionChanged -= OnBlockPositionChanged;
    }

    internal void OnBlockPositionChanged(object sender, PositionChangedEventArgs e)
    {
        var oldPath = finishPoint.position - transform.position;
        //Находим, нужно ли смещать спавнер относительно пути для него и изменения позиции блока на башне
        // Синус позволяет понять, насколько смещать путь, чтобы он вновь проходил через центр последнего блока на башне
        var spawnerPositionDelta = Mathf.Sin(Mathf.Deg2Rad * Vector3.Angle(e.PositionDelta, oldPath)) * e.PositionDelta;
        transform.position += spawnerPositionDelta;
        finishPoint.position += spawnerPositionDelta;
    }

    public void Spawn()
    {
        var block = _blockFactory.Create(transform.position, finishPoint.position);
        block.transform.SetParent(transform, true);
        if (_blockSizeSetter.ShouldApplySize())
            _blockSizeSetter.ApplySize(block.transform);
    }
}