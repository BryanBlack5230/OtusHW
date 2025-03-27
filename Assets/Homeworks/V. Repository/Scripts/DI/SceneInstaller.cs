using System.Collections.Generic;
using GameEngine;
using SaveSystem;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ResourceService>().AsSingle().NonLazy();
        Container.Bind<UnitManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameRepository>().AsSingle().NonLazy();

        BindSaveLoaders();
        
        Container.Bind<Resource>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<Unit>().FromComponentsInHierarchy().AsSingle();
        
        Container.Bind<Helper>().FromComponentInHierarchy().AsSingle();
    }

    private void BindSaveLoaders()
    {
        Container.Bind<ISaveLoader>().To<ResourceSaveLoader>().AsSingle();
    }

}
