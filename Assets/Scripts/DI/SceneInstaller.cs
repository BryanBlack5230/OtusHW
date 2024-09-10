using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField] private Transform _playerTransform;
		[SerializeField] private GameObject _enemyPrefab;
		[SerializeField] private Bullet _bulletPrefab;
		
		public override void InstallBindings()
		{
			Container.Bind<Transform>().WithId("Player").FromInstance(_playerTransform).AsSingle();
			
			Container.Bind<GameLoopManager>().FromComponentInHierarchy().AsSingle();
			Container.Bind<GameManagerUIController>().FromComponentInHierarchy().AsSingle();
			Container.Bind<GameManager>().AsSingle();
			
			Container.Bind<InputManager>().AsSingle();
			
			Container.Bind<EnemyInstaller>().AsSingle();
			Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();
			

			Container.Bind<BulletSystem>().FromComponentInHierarchy().AsSingle();
			Container.Bind<EnemyPositions>().FromComponentInHierarchy().AsSingle();
			Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();
			
			Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().WithArguments(_enemyPrefab);
			Container.Bind<IBulletFactory>().To<BulletFactory>().AsSingle().WithArguments(_bulletPrefab);

		}
	}
}