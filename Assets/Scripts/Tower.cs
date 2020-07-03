using System;
using UnityEngine;

public class Tower : MonoBehaviour, ITower
{
    public event EventHandler BlockAttached;
    
    private void OnTransformChildrenChanged()
    {
        BlockAttached?.Invoke(this, EventArgs.Empty);
    }
}
