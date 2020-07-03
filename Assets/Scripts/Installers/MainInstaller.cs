using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] 
    private BlockSpawnStarter blockSpawnersStarter;
    
    [SerializeField] 
    private Tower tower;
    
    public override void InstallBindings()
    {
        Container.Bind<ITower>().FromInstance(tower).AsSingle();
        Container.Bind<IBlockSpawnStarter>().FromInstance(blockSpawnersStarter).AsSingle();
        Container.BindInterfacesTo<GameOverHandler>().AsSingle();
        Container.BindInterfacesTo<StartGameHandler>().AsSingle();
    }
}