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

		private GameObject _enemy;
		private EnemyMoveAgent _moveAgent;
		private HitPointsComponent _hp;
		private EnemyAttackAgent _attackAgent;
		
		public EnemyInstaller(EnemyPositions enemyPositions, BulletSystem bulletSystem, [Inject(Id = "Player")] Transform character)
		{
			_enemyPositions = enemyPositions;
			_bulletSystem = bulletSystem;
			_character = character;
		}

		public void Install(GameObject enemy, Action<GameObject> onDestroyed)
		{
			if (enemy == null) return;

			_enemy = enemy;
			SetEnemyTransform();

			InstallHitPoints(onDestroyed);
			InstallMoveAgent();
			InstallAttackAgent();
		}

		private void InstallHitPoints(Action<GameObject> onDestroyed)
		{
			_hp = _enemy.GetComponent<HitPointsComponent>();
			_hp.HpEmpty += onDestroyed;
		}

		public void Uninstall(GameObject enemy, Action<GameObject> onDestroyed)
		{
			if (enemy == null) return;

			if (enemy.TryGetComponent<HitPointsComponent>(out var hp))
			{
				hp.HpEmpty -= onDestroyed;
				hp.Reset();
			}
			
			if (enemy.TryGetComponent<EnemyAttackAgent>(out var attackAgent))
			{
				attackAgent.Reset();
			}
		}

		public EnemyAttackAgent GetAttackAgent() => _attackAgent;

		private void SetEnemyTransform()
		{
			_enemy.transform.position = _enemyPositions.RandomSpawnPosition().position;
		}

		private void InstallMoveAgent()
		{
			_moveAgent = _enemy.GetComponent<EnemyMoveAgent>();

			_moveAgent.SetDestination(_enemyPositions.RandomAttackPosition().position);
		}
		
		private void InstallAttackAgent()
		{
			_attackAgent = _enemy.GetComponent<EnemyAttackAgent>();

			_attackAgent.SetTarget(_character);
			_attackAgent.SetBulletSystem(_bulletSystem);
			_attackAgent.IsAbleToShoot
							.Append(_moveAgent.IsReached)
							.Append(_hp.IsHitPointsExists);
		}
	}
}