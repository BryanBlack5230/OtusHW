using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class EnemySpawner: MonoBehaviour, IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener
	{
		[SerializeField] private Transform _activeContainer;
		[SerializeField] private Transform _inactiveContainer;
		private EnemyInitializer _enemyInitializer;
		private Pool _enemyPool;
		private const int ENEMY_AMOUNT = 7;
		private readonly Dictionary<EnemyFacade, EnemyAttackAgent> _activeEnemies = new();
		private Coroutine _spawnCouroutine;

		[Inject]
		public void Construct(IEnemyFactory enemyFactory, EnemyInitializer enemyInstaller)
		{
			_enemyPool = new(() => enemyFactory.Create(), ENEMY_AMOUNT, isFixedAmount: true, _activeContainer, _inactiveContainer);
			_enemyInitializer = enemyInstaller;
		}
		
		private IEnumerator Spawn()
		{
			while (true)
			{
				yield return new WaitForSeconds(1);
				var enemyGameObject = _enemyPool.Spawn();
				if (enemyGameObject == null) continue;

				if (enemyGameObject.TryGetComponent<EnemyFacade>(out EnemyFacade enemy))
				{
					if (!_activeEnemies.ContainsKey(enemy))
					{
						_enemyInitializer.Initialize(enemy, OnEnemyDestroyed);
						_activeEnemies.Add(enemy, enemy.AttackAgent);
					}
				}
			}
		}

		private void OnEnemyDestroyed(GameObject enemyGameObject)
		{
			if (enemyGameObject.TryGetComponent<EnemyFacade>(out EnemyFacade enemy))
			{
				if (_activeEnemies.Remove(enemy))
				{
					_enemyInitializer.Reset(enemy, OnEnemyDestroyed);
					_enemyPool.Return(enemyGameObject);
				}
			}
		}
		
		public Dictionary<EnemyFacade, EnemyAttackAgent> GetActiveEnemies() => _activeEnemies;

		public void OnStartGame()
		{
			_spawnCouroutine = StartCoroutine(Spawn());
		}

		public void OnFinishGame()
		{
			StopCoroutine(_spawnCouroutine);
		}

		public void OnPause()
		{
			StopCoroutine(_spawnCouroutine);
		}

		public void OnResume()
		{
			_spawnCouroutine = StartCoroutine(Spawn());
		}
	}
}