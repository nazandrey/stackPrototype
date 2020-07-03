using UnityEngine;
using Zenject;

public class BlockPrefabInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject blockPrefab;
        
    public override void InstallBindings()
    {
        Container.BindFactory<Vector3, Vector3, Block, Block.Factory>().FromComponentInNewPrefab(blockPrefab);
    }
}