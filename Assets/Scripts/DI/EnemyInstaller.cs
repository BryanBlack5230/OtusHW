using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class EnemyInstaller : MonoInstaller
	{
		[SerializeField] private float _enemyFireCooldown;
		public override void InstallBindings()
		{
			Container.Bind<WeaponComponent>().FromComponentInHierarchy().AsSingle();
			Container.Bind<MoveComponent>().FromComponentInHierarchy().AsSingle();
			Container.Bind<HitPointsComponent>().FromComponentInHierarchy().AsSingle();
			Container.Bind<EnemyAttackAgent>().AsSingle().WithArguments(_enemyFireCooldown);
			Container.BindInterfacesAndSelfTo<EnemyMoveAgent>().AsSingle().NonLazy();
			Container.Bind<EnemyFacade>().FromComponentInHierarchy().AsSingle();
		}
	}
}