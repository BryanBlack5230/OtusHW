using System;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class EnemyAttackAgent : MonoBehaviour
	{
		public CompositeCondition IsAbleToShoot {get; private set;} = new();
		private WeaponComponent _weaponComponent;
		private float _countdown;

		private Transform _target;
		private BulletSystem _bulletSystem;
		private float _currentTime;
		
		public void Constructor(WeaponComponent weaponComponent, float countdown)
		{
			_weaponComponent = weaponComponent;
			_countdown = countdown;
		}

		public void SetTarget(Transform target)
		{
			_target = target;
		}

		public void SetBulletSystem(BulletSystem bulletSystem)
		{
			_bulletSystem = bulletSystem;
		}

		public void Reset()
		{
			_currentTime = _countdown;
			IsAbleToShoot = new();
		}

		public void OnUpdate(float time)
		{
			if (!IsAbleToShoot.IsTrue()) return;

			UpdateCooldown(time);

			if (!IsCooldown())
				Fire();
		}

		private void UpdateCooldown(float time)
		{
			_currentTime -= time;
		}

		private bool IsCooldown()
		{
			return _currentTime > 0;
		}

		public void Fire()
		{
			_currentTime += _countdown;
			
			var startPosition = _weaponComponent.Position;
			var vector = (Vector2) _target.position - startPosition;
			var direction = vector.normalized;

			_bulletSystem.Shoot(new BulletSystem.ShootArgs
			{
				isPlayer = false,
				physicsLayer = (int) PhysicsLayer.ENEMY_BULLET,
				color = Color.red,
				damage = 1,
				position = startPosition,
				velocity = direction * 2.0f
			});
		}
	}
}