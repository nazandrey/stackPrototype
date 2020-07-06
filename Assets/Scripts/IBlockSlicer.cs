using System;
using UnityEngine;

public interface IBlockSlicer
{
    event EventHandler<PositionChangedEventArgs> BlockPositionChanged;
    void Slice(Transform transformToSlice, ContactPoint[] contactPoints);
}