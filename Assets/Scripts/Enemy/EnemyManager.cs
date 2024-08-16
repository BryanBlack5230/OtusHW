using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class EnemyManager : MonoBehaviour
	{
		[SerializeField] private Transform _activeContainer;
		[SerializeField] private Transform _inactiveContainer;
		[SerializeField] private GameObject _prefab;
		[SerializeField] private EnemyInstaller _enemyInstaller;
		private Pool _enemyPool;
		private const int ENEMY_AMOUNT = 7;
		private readonly Dictionary<GameObject, EnemyAttackAgent> _activeEnemiesDict = new();

		private void Awake() 
		{
			_enemyPool = new(_prefab, ENEMY_AMOUNT, isFixedAmount: true, _activeContainer, _inactiveContainer);
		}
		private IEnumerator Start()
		{
			while (true)
			{
				yield return new WaitForSeconds(1);
				var enemy = _enemyPool.Get();
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
				_enemyPool.Return(enemy);
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