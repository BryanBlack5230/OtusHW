using System.Collections.Generic;
using GameEngine;
using SaveSystem;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Transform _unitsContainer;
    public override void InstallBindings()
    {
        Container.Bind<ResourceService>().AsSingle().NonLazy();
        Container.Bind<UnitManager>().AsSingle().NonLazy();
        Container.Bind<UnitFactory>().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<GameRepository>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameEncryptedRepository>().AsSingle().NonLazy();

        BindSaveLoaders();
        
        Container.Bind<Resource>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<Unit>().FromComponentsInHierarchy().AsSingle();
        
        Container.Bind<Helper>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SceneInitializer>().AsSingle().WithArguments(_unitsContainer).NonLazy();
    }

    private void BindSaveLoaders()
    {
        Container.Bind<ISaveLoader>().To<ResourceSaveLoader>().AsSingle();
        Container.Bind<ISaveLoader>().To<UnitSaveLoader>().AsSingle();
    }

}
