using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class EnemyManager : MonoBehaviour
	{
		[SerializeField] private EnemyPool _enemyPool;
		[SerializeField] private EnemyInstaller _enemyInstaller;
		
		private readonly Dictionary<GameObject, EnemyAttackAgent> _activeEnemiesDict = new();

		private IEnumerator Start()
		{
			while (true)
			{
				yield return new WaitForSeconds(1);
				var enemy = _enemyPool.SpawnEnemy();
				if (enemy == null) continue;

				
				if (!_activeEnemiesDict.ContainsKey(enemy))
				{
					_enemyInstaller.Install(enemy, OnDestroyed);
					_activeEnemiesDict.Add(enemy, _enemyInstaller.GetAttackAgent());
				}
			}
		}

		private void OnDestroyed(GameObject enemy)
		{
			if (_activeEnemiesDict.Remove(enemy))
			{
				_enemyInstaller.Uninstall(enemy, OnDestroyed);
				_enemyPool.UnspawnEnemy(enemy);
			}
		}

		private void FixedUpdate() 
		{
			foreach (var enemy in _activeEnemiesDict)
			{
				var attackAgent = enemy.Value;
				if (attackAgent == null) continue;
				
				attackAgent.UpdateFiringSequence(Time.fixedDeltaTime);
			}
		}
	}
}