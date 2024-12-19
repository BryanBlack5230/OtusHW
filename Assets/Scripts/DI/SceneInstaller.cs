using UnityEngine;
using Zenject;

namespace ShootEmUp
{

	public class SceneInstaller : MonoInstaller
	{
		[SerializeField] private Transform _playerTransform;
		[SerializeField] private Bullet _bulletPrefab;
		[SerializeField] private GameObject _enemyPrefab;
		
		public override void InstallBindings()
		{
			Container.Bind<Transform>().WithId("Player").FromInstance(_playerTransform).AsSingle();

			Container.Bind<GameLoopManager>().FromComponentInHierarchy().AsSingle();
			Container.Bind<GameManagerUIController>().FromComponentInHierarchy().AsSingle();
			Container.Bind<GameManager>().AsSingle();

			BindCharacter();

			Container.BindInterfacesAndSelfTo<BulletSystem>().FromComponentInHierarchy().AsSingle();
			Container.Bind<IBulletFactory>().To<BulletFactory>().AsSingle().WithArguments(_bulletPrefab);

			Container.Bind<EnemyPositions>().FromComponentInHierarchy().AsSingle();
			Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();
			Container.BindInterfacesTo<LevelBackground>().FromComponentInHierarchy().AsSingle();

			BindEnemy();
		}

		private void BindEnemy()
		{
			Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().WithArguments(_enemyPrefab);
			Container.Bind<EnemyInitializer>().AsSingle();
			Container.BindInterfacesAndSelfTo<EnemySpawner>().FromComponentInHierarchy().AsSingle();
			Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle().NonLazy();
		}

		private void BindCharacter()
		{
			Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
			Container.BindInterfacesAndSelfTo<CharacterShooterController>().FromComponentInHierarchy().AsSingle();
			Container.BindInterfacesAndSelfTo<CharacterMoveController>().FromComponentInHierarchy().AsSingle();
			Container.BindInterfacesAndSelfTo<CharacterDeathObserver>().AsSingle().NonLazy();
		}
	}
}