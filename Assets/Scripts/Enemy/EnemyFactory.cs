using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public interface IEnemyFactory
	{
		GameObject Create();
	}
	
	public class EnemyFactory : IEnemyFactory
	{
		private readonly DiContainer _container;
		private readonly GameObject _enemyPrefab;
		private readonly Transform _inactiveContainer;
		
		public EnemyFactory(DiContainer container, GameObject enemyPrefab, Transform inactiveContainer)
		{
			_container = container;
			_enemyPrefab = enemyPrefab;
			_inactiveContainer = inactiveContainer;
		}

		public GameObject Create()
		{
			var enemy = _container.InstantiatePrefab(_enemyPrefab, _inactiveContainer);
			return enemy;
		}
	}
}