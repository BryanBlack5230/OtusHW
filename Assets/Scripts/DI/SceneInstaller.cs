using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField] private InputConfig _inputConfig;
		public override void InstallBindings()
		{
			Container.Bind<GameLoopManager>().FromComponentInHierarchy().AsSingle();
			Container.Bind<GameManagerUIController>().FromComponentInHierarchy().AsSingle();
			Container.Bind<GameManager>().AsSingle();
			
			Container.Bind<InputManager>().AsSingle();
			Container.Bind<InputConfig>().FromInstance(_inputConfig);

			Container.Bind<BulletSystem>().FromComponentInHierarchy().AsSingle();

			EnemyBindingsInstaller.Install(Container);
		}
	}
}