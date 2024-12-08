using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public sealed class EnemyMoveAgent : IGameFixedUpdateListener
	{
		public Func<bool> IsReached
		{
			get { return () => _isReached; }
		}

		private MoveComponent _moveComponent;
		private Vector2 _destination;
		private bool _isReached;
		private Transform _transform;
		
		public EnemyMoveAgent(MoveComponent moveComponent)
		{
			IGameListener.Register(this);
			
			_moveComponent = moveComponent;
		}
		
		public void SetTransform(Transform transform)
		{
			_transform = transform;
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
			
			var vector = _destination - (Vector2) _transform.position;
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