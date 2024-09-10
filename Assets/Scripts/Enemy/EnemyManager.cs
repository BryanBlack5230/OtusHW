using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public sealed class EnemyManager : MonoBehaviour, IGameFixedUpdateListener
	{
		private EnemySpawner _enemySpawner;
		
		[Inject]
		public void Construct(EnemySpawner enemySpawner)
		{
			_enemySpawner = enemySpawner;
		}
		
		private void Awake() 
		{
			IGameListener.Register(this);
		}

		public void OnFixedUpdate(float fixedDeltaTime)
		{
			foreach (var enemy in _enemySpawner.GetActiveEnemies())
			{
				var attackAgent = enemy.Value;
				if (attackAgent == null) continue;
				
				attackAgent.OnUpdate(fixedDeltaTime);
			}
		}
	}
}