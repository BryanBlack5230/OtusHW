using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public sealed class Enemy : MonoBehaviour
	{
		public EnemyAttackAgent AttackAgent {private set; get;}
		public EnemyMoveAgent MoveAgent {private set; get;}
		public HitPointsComponent HitPoints {private set; get;}
		
		[Inject]
		public void Constructor(EnemyAttackAgent enemyAttackAgent, EnemyMoveAgent moveAgent, HitPointsComponent hitPoints)
		{
			AttackAgent = enemyAttackAgent; // WeaponComponent
			MoveAgent = moveAgent; // MoveComponent, transform
			HitPoints = hitPoints;
		}
	}
}