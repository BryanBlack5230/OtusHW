using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawner: MonoBehaviour
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
		
		private void Start()
		{
			StartCoroutine(Spawn());
		}
		
		private IEnumerator Spawn()
		{
			while (true)
			{
				yield return new WaitForSeconds(1);
				var enemy = _enemyPool.Spawn();
				if (enemy == null) continue;

				
				if (!_activeEnemiesDict.ContainsKey(enemy))
				{
					_enemyInstaller.Install(enemy, OnEnemyDestroyed);
					_activeEnemiesDict.Add(enemy, _enemyInstaller.GetAttackAgent());
				}
			}
		}

		private void OnEnemyDestroyed(GameObject enemy)
		{
			if (_activeEnemiesDict.Remove(enemy))
			{
				_enemyInstaller.Uninstall(enemy, OnEnemyDestroyed);
				_enemyPool.Return(enemy);
			}
		}
		
		public Dictionary<GameObject, EnemyAttackAgent> GetActiveEnemies() => _activeEnemiesDict;
	}
}