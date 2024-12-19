using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class EnemyInitializer
	{
		private EnemyPositions _enemyPositions;
		private Transform _character;
		private BulletSystem _bulletSystem;

		private EnemyFacade _enemy;
		
		public EnemyInitializer(EnemyPositions enemyPositions, BulletSystem bulletSystem, [Inject(Id = "Player")] Transform character)
		{
			_enemyPositions = enemyPositions;
			_bulletSystem = bulletSystem;
			_character = character;
		}

		public void Initialize(EnemyFacade enemy, Action<GameObject> onDestroyed)
		{
			_enemy = enemy;
			InitEnemyTransform();

			InitHitPoints(onDestroyed);
			InitMoveAgent();
			InitAttackAgent();
		}

		private void InitHitPoints(Action<GameObject> onDestroyed)
		{
			_enemy.HitPoints.HpEmpty += onDestroyed;
		}

		public void Reset(EnemyFacade enemy, Action<GameObject> onDestroyed)
		{
			if (!enemy) return;

			enemy.HitPoints.HpEmpty -= onDestroyed;
			enemy.HitPoints.Reset();
			
			enemy.AttackAgent.Reset();
		}

		public EnemyAttackAgent GetAttackAgent() => _enemy.AttackAgent;

		private void InitEnemyTransform()
		{
			_enemy.transform.position = _enemyPositions.RandomSpawnPosition().position;
		}

		private void InitMoveAgent()
		{
			_enemy.MoveAgent.SetTransform(_enemy.transform);
			_enemy.MoveAgent.SetDestination(_enemyPositions.RandomAttackPosition().position);
		}
		
		private void InitAttackAgent()
		{
			_enemy.AttackAgent.SetTarget(_character);
			_enemy.AttackAgent.SetBulletSystem(_bulletSystem);
			_enemy.AttackAgent.IsAbleToShoot
							.Append(_enemy.MoveAgent.IsReached)
							.Append(_enemy.HitPoints.IsHitPointsExists);
		}
	}
}