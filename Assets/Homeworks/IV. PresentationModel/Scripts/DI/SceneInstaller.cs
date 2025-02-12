using Lessons.Architecture.PM;
using UnityEngine;
using Zenject;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CharacterInfo>().AsSingle().NonLazy();
        // Container.Bind<CharacterStat>().AsSingle().NonLazy();
        Container.Bind<PlayerLevel>().AsSingle().NonLazy();
        Container.Bind<UserInfo>().AsSingle().NonLazy();
        
        Container.Bind<HeroPresenterFactory>().AsSingle().NonLazy();
    }
}
