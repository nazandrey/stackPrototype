using System;
using System.Linq;
using UnityEngine;

public class BlockSlicer : IBlockSlicer
{
    private IBlockSizeSetter _blockSizeSetter;

    public BlockSlicer(IBlockSizeSetter blockSizeSetter)
    {
        _blockSizeSetter = blockSizeSetter;
    }

    public event EventHandler<PositionChangedEventArgs> BlockPositionChanged;

    public void Slice(Transform transformToSlice, ContactPoint[] contactPoints)
    {
        var minX = contactPoints.Min(contactPoint => contactPoint.point.x);
        var maxX = contactPoints.Max(contactPoint => contactPoint.point.x);
        var minZ = contactPoints.Min(contactPoint => contactPoint.point.z);
        var maxZ = contactPoints.Max(contactPoint => contactPoint.point.z);

        var oldPosition = transformToSlice.position;
        var newPosition = new Vector3((maxX + minX) / 2, oldPosition.y, (maxZ + minZ) / 2);
        var positionDelta = oldPosition - newPosition;
        transformToSlice.position = newPosition;
        BlockPositionChanged?.Invoke(this, new PositionChangedEventArgs(positionDelta));
        
        var newScale = new Vector3(maxX - minX, transformToSlice.localScale.y, maxZ - minZ);
        transformToSlice.localScale = newScale;
        _blockSizeSetter.SetBlockTemplateSize(newScale);
    }
}