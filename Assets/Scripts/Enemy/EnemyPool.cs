using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class EnemyPool : MonoBehaviour
	{
		[Header("Pool")][SerializeField] private Transform _container;
		[SerializeField] private Transform _worldTransform;

		[SerializeField] private GameObject _prefab;

		private readonly Queue<GameObject> _enemyPool = new();
		private Pool<GameObject> _pool;
		private const int ENEMY_AMOUNT = 7;
		
		private void Awake()
		{
			_pool = new(_prefab, ENEMY_AMOUNT, isFixedAmount: true, _worldTransform, _container);
		}

		public GameObject SpawnEnemy()
		{
			return _pool.Get();
			if (!_enemyPool.TryDequeue(out var enemy))
				return null;
			
			return enemy;
		}

		public void UnspawnEnemy(GameObject enemy)
		{
			_pool.Return(enemy);
		}
	}
}