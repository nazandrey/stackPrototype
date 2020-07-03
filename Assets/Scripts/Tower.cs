using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public event EventHandler BlockAttached;
    
    private void OnTransformChildrenChanged()
    {
        BlockAttached?.Invoke(this, EventArgs.Empty);
    }
}
