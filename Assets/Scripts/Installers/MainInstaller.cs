using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] 
    private BlockSpawner blockSpawner;
    
    [SerializeField] 
    private Tower tower;
    
    public override void InstallBindings()
    {
        Container.Bind<IBlockSpawner>().FromInstance(blockSpawner);
        Container.Bind<ITower>().FromInstance(tower);
        Container.BindInterfacesTo<GameOverHandler>().AsSingle();
        Container.BindInterfacesTo<StartGameHandler>().AsSingle();
    }
}