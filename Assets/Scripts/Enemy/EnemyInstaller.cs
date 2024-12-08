using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class EnemyInstaller
	{
		private EnemyPositions _enemyPositions;
		private Transform _character;
		private BulletSystem _bulletSystem;

		private Enemy _enemy;
		
		public EnemyInstaller(EnemyPositions enemyPositions, BulletSystem bulletSystem, [Inject(Id = "Player")] Transform character)
		{
			_enemyPositions = enemyPositions;
			_bulletSystem = bulletSystem;
			_character = character;
		}

		public void Install(Enemy enemy, Action<GameObject> onDestroyed)
		{
			_enemy = enemy;
			SetEnemyTransform();

			InstallHitPoints(onDestroyed);
			InstallMoveAgent();
			InstallAttackAgent();
		}

		private void InstallHitPoints(Action<GameObject> onDestroyed)
		{
			_enemy.HitPoints.HpEmpty += onDestroyed;
		}

		public void Uninstall(Enemy enemy, Action<GameObject> onDestroyed)
		{
			if (!enemy) return;

			enemy.HitPoints.HpEmpty -= onDestroyed;
			enemy.HitPoints.Reset();
			
			enemy.AttackAgent.Reset();
		}

		public EnemyAttackAgent GetAttackAgent() => _enemy.AttackAgent;

		private void SetEnemyTransform()
		{
			_enemy.transform.position = _enemyPositions.RandomSpawnPosition().position;
		}

		private void InstallMoveAgent()
		{
			_enemy.MoveAgent.SetTransform(_enemy.transform);
			_enemy.MoveAgent.SetDestination(_enemyPositions.RandomAttackPosition().position);
		}
		
		private void InstallAttackAgent()
		{
			_enemy.AttackAgent.SetTarget(_character);
			_enemy.AttackAgent.SetBulletSystem(_bulletSystem);
			_enemy.AttackAgent.IsAbleToShoot
							.Append(_enemy.MoveAgent.IsReached)
							.Append(_enemy.HitPoints.IsHitPointsExists);
		}
	}
}