using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
	{
		[SerializeField] EnemySpawner _enemySpawner;
		private void FixedUpdate() 
		{
			foreach (var enemy in _enemySpawner.GetActiveEnemies())
			{
				var attackAgent = enemy.Value;
				if (attackAgent == null) continue;
				
				attackAgent.OnUpdate(Time.fixedDeltaTime);
			}
		}
	}
}