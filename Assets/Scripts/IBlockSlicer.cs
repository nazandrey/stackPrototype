using UnityEngine;

public interface IBlockSlicer
{
    void Slice(Transform transformToSlice, ContactPoint[] contactPoints);
}