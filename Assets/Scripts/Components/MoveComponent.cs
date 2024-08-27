using UnityEngine;

namespace ShootEmUp
{
	public sealed class MoveComponent
	{
        public Vector3 Position => _rigidbody2D.position;
		private readonly Rigidbody2D _rigidbody2D;
		private readonly float _speed;

		public MoveComponent(Rigidbody2D rigidbody2D, float speed)
		{
			_rigidbody2D = rigidbody2D;
			_speed = speed;
		}

		public void Move(Vector2 vector)
		{
			var nextPosition = _rigidbody2D.position + vector * _speed;
			_rigidbody2D.MovePosition(nextPosition);
		}

    }
}