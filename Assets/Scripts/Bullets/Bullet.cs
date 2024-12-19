using System;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class Bullet : MonoBehaviour
	{
		public event Action<Bullet, Collision2D> OnCollisionEntered;

		[NonSerialized] public bool isPlayer;
		[NonSerialized] public int damage;

		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField] private SpriteRenderer _spriteRenderer;
		private Vector2 _velocity;

		private void OnCollisionEnter2D(Collision2D collision)
		{
			OnCollisionEntered?.Invoke(this, collision);
		}

		public void SetVelocity(Vector2 velocity)
		{
			_velocity = velocity;
			_rigidbody2D.velocity = velocity;
		}

		public void SetPhysicsLayer(int physicsLayer)
		{
			gameObject.layer = physicsLayer;
		}

		public void SetPosition(Vector3 position)
		{
			transform.position = position;
		}

		public void SetColor(Color color)
		{
			_spriteRenderer.color = color;
		}

		public void OnPause()
		{
			_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
			_rigidbody2D.velocity = Vector2.zero;
		}

		public void OnResume()
		{
			_rigidbody2D.constraints = RigidbodyConstraints2D.None;
			_rigidbody2D.velocity = _velocity;
		}

	}
}