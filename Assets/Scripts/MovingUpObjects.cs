using System;
using UnityEngine;

public class MovingUpObjects : MonoBehaviour
{
    [SerializeField]
    private Transform heightDeltaObject;
    
    [SerializeField] 
    private Tower tower;

    private void Awake()
    {
        tower.BlockAttached += OnBlockAttached;
    }

    private void OnDestroy()
    {
        tower.BlockAttached -= OnBlockAttached;
    }

    private void OnBlockAttached(object sender, EventArgs e)
    {
        MoveUp();
    }

    public void MoveUp()
    {
        transform.Translate(new Vector3(0, heightDeltaObject.localScale.y), Space.World);
    }
}
