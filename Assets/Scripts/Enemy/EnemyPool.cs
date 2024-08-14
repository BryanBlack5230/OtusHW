using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class EnemyPool : MonoBehaviour
	{
		[Header("Pool")][SerializeField] private Transform _container;

		[SerializeField] private GameObject _prefab;

		private readonly Queue<GameObject> _enemyPool = new();
		private const int ENEMY_AMOUNT = 7;
		
		private void Awake()
		{
			for (var i = 0; i < ENEMY_AMOUNT; i++)
			{
				var enemy = Instantiate(_prefab, _container);
				_enemyPool.Enqueue(enemy);
			}
		}

		public GameObject SpawnEnemy()
		{
			if (!_enemyPool.TryDequeue(out var enemy))
				return null;
			
			return enemy;
		}

		public void UnspawnEnemy(GameObject enemy)
		{
			enemy.transform.SetParent(_container);
			_enemyPool.Enqueue(enemy);
		}
	}
}