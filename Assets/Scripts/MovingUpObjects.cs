using System;
using UnityEngine;
using Zenject;

public class MovingUpObjects : MonoBehaviour
{
    [SerializeField]
    private Transform heightDeltaObject;
    
    private ITower _tower;

    [Inject]
    public void Construct(ITower tower)
    {
        _tower = tower;
    }
    
    private void Awake()
    {
        _tower.BlockAttached += OnBlockAttached;
    }

    private void OnDestroy()
    {
        _tower.BlockAttached -= OnBlockAttached;
    }

    private void OnBlockAttached(object sender, EventArgs e)
    {
        MoveUp();
    }

    private void MoveUp()
    {
        transform.Translate(new Vector3(0, heightDeltaObject.localScale.y), Space.World);
    }
}
