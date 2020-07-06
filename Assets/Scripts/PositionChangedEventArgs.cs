using System;
using UnityEngine;

public class PositionChangedEventArgs : EventArgs
{
    public PositionChangedEventArgs(Vector3 positionDelta)
    {
        PositionDelta = positionDelta;
    }

    public Vector3 PositionDelta { get; }
}