using System;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class EnemyMoveAgent : MonoBehaviour, IGameFixedUpdateListener
	{
		public Func<bool> IsReached
		{
			get { return () => _isReached; }
		}

		private MoveComponent _moveComponent;
		private Vector2 _destination;
		private bool _isReached;
		
		public void Constructor(MoveComponent moveComponent)
		{
			_moveComponent = moveComponent;
		}

		private void Awake() 
		{
			IGameListener.Register(this);
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
			
			var vector = _destination - (Vector2) transform.position;
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