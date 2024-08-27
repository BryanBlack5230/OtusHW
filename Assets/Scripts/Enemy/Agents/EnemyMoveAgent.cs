using System;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class EnemyMoveAgent : IGameFixedUpdateListener
	{
		public Func<bool> IsReached
		{
			get { return () => _isReached; }
		}

		private readonly MoveComponent _moveComponent;
		private Vector2 _destination;
		private bool _isReached;

		public EnemyMoveAgent(MoveComponent moveComponent)
		{
			_moveComponent = moveComponent;
		}
		
		public void SetDestination(Vector2 endPoint)
		{
			_destination = endPoint;
			_isReached = false;
		}

		public void OnFixedUpdate(float fixedDeltaTime)
		{
			if (_isReached)
			{
				return;
			}
			
			var vector = _destination - (Vector2) _moveComponent.Position;
			if (vector.magnitude <= 0.25f)
			{
				_isReached = true;
				return;
			}

			var direction = vector.normalized * fixedDeltaTime;
			_moveComponent.Move(direction);
		}
	}
}