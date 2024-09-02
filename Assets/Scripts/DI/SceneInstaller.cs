using Zenject;

namespace ShootEmUp
{
    public class SceneInstaller : MonoInstaller
	{
		
		public override void InstallBindings()
		{
			Container.Bind<GameLoopManager>().FromComponentInHierarchy().AsSingle();
			Container.Bind<GameManagerUIController>().FromComponentInHierarchy().AsSingle();
			Container.Bind<GameManager>().AsSingle();
			
			Container.Bind<InputManager>().AsSingle();

			Container.Bind<BulletSystem>().FromComponentInHierarchy().AsSingle();
			Container.Bind<EnemyPositions>().FromComponentInHierarchy().AsSingle();
			Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();
		}
	}
}