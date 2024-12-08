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
		private EnemyInstaller _enemyInstaller;
		private Pool _enemyPool;
		private const int ENEMY_AMOUNT = 7;
		private readonly Dictionary<Enemy, EnemyAttackAgent> _activeEnemiesDict = new();
		private Coroutine _spawnCouroutine;

		[Inject]
		public void Construct(IEnemyFactory enemyFactory, EnemyInstaller enemyInstaller)
		{
			_enemyPool = new(() => enemyFactory.Create(), ENEMY_AMOUNT, isFixedAmount: true, _activeContainer, _inactiveContainer);
			_enemyInstaller = enemyInstaller;
		}
		private void Awake() 
		{
			IGameListener.Register(this);
		}
		
		private IEnumerator Spawn()
		{
			while (true)
			{
				Debug.Log("while loop begin");
				yield return new WaitForSeconds(1);
				Debug.Log("spawn enemy");
				var enemy = _enemyPool.Spawn();
				if (enemy == null) continue;

				if (enemy.TryGetComponent<Enemy>(out Enemy enemyComponent))
				{
					if (!_activeEnemiesDict.ContainsKey(enemyComponent))
					{
						_enemyInstaller.Install(enemyComponent, OnEnemyDestroyed);
						_activeEnemiesDict.Add(enemyComponent, enemyComponent.AttackAgent);
					}
				}
			}
		}

		private void OnEnemyDestroyed(GameObject enemy)
		{
			if (enemy.TryGetComponent<Enemy>(out Enemy enemyComponent))
			{
				if (_activeEnemiesDict.Remove(enemyComponent))
				{
					_enemyInstaller.Uninstall(enemyComponent, OnEnemyDestroyed);
					_enemyPool.Return(enemy);
				}
			}
		}
		
		public Dictionary<Enemy, EnemyAttackAgent> GetActiveEnemies() => _activeEnemiesDict;

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