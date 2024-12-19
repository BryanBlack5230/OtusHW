using Zenject;

namespace ShootEmUp
{
	public sealed class EnemyManager : IGameFixedUpdateListener
	{
		private EnemySpawner _enemySpawner;
		
		public EnemyManager(EnemySpawner enemySpawner)
		{
			_enemySpawner = enemySpawner;
		}

		public void OnFixedUpdate(float fixedDeltaTime)
		{
			foreach (var enemy in _enemySpawner.GetActiveEnemies())
			{
				var facade = enemy.Key;
				
				facade.AttackAgent.OnUpdate(fixedDeltaTime);
				facade.MoveAgent.OnUpdate(fixedDeltaTime);
			}
		}
	}
}