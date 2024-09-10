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
		private readonly Dictionary<GameObject, EnemyAttackAgent> _activeEnemiesDict = new();
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