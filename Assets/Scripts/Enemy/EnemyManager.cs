using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class EnemyManager : MonoBehaviour, IGameFixedUpdateListener
	{
		[SerializeField] EnemySpawner _enemySpawner;
		
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