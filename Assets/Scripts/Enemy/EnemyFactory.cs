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
		
		public EnemyFactory(DiContainer container, GameObject enemyPrefab)
		{
			_container = container;
			_enemyPrefab = enemyPrefab;
		}

		public GameObject Create()
		{
			var enemy = _container.InstantiatePrefab(_enemyPrefab);
			return enemy;
		}
	}
}