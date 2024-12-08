using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField] private Transform _playerTransform;
		[SerializeField] private GameObject _enemyPrefab;
		[SerializeField] private Bullet _bulletPrefab;
		[SerializeField] private float enemyFireCooldown;
		
		public override void InstallBindings()
		{
			Container.Bind<Transform>().WithId("Player").FromInstance(_playerTransform).AsSingle();
			
			Container.Bind<GameLoopManager>().FromComponentInHierarchy().AsSingle();
			Container.Bind<GameManagerUIController>().FromComponentInHierarchy().AsSingle();
			Container.Bind<GameManager>().AsSingle();
			
			Container.Bind<InputManager>().AsSingle();
			
			

			Container.Bind<BulletSystem>().FromComponentInHierarchy().AsSingle();
			Container.Bind<EnemyPositions>().FromComponentInHierarchy().AsSingle();
			Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();
			
			Container.Bind<IBulletFactory>().To<BulletFactory>().AsSingle().WithArguments(_bulletPrefab);
			
			EnemyBindings();
		}
		
		private void EnemyBindings()
		{
			Container.Bind<WeaponComponent>().FromComponentInNewPrefab(_enemyPrefab).AsTransient();
			Container.Bind<MoveComponent>().FromComponentInNewPrefab(_enemyPrefab).AsTransient();
			Container.Bind<HitPointsComponent>().FromComponentInNewPrefab(_enemyPrefab).AsTransient();
			Container.Bind<EnemyAttackAgent>().AsTransient().WithArguments(enemyFireCooldown); // not monobeh
			Container.Bind<EnemyMoveAgent>().AsTransient(); // not monobeh
			
			Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().WithArguments(_enemyPrefab);
			Container.Bind<EnemyInstaller>().AsSingle();
			Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();
		}
	}
}