using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ShootEmUp
{
	public sealed class BulletSystem : MonoBehaviour
	{
		[SerializeField] private int _initialCount = 50;
		[SerializeField] private Transform _inactiveContainer;
		[SerializeField] private Bullet _prefab;
		[SerializeField] private Transform _activeContainer;
		[SerializeField] private LevelBounds _levelBounds;

		private Pool _bulletPool;
		private readonly HashSet<Bullet> _activeBullets = new();
		private readonly List<Bullet> _cache = new();
		
		private void Awake()
		{
			_bulletPool = new(_prefab.gameObject, _initialCount, isFixedAmount: false, _activeContainer, _inactiveContainer);
		}
		
		private void FixedUpdate()
		{
			_cache.Clear();
			_cache.AddRange(_activeBullets);

			for (int i = 0, count = _cache.Count; i < count; i++)
			{
				var bullet = _cache[i];
				if (!_levelBounds.InBounds(bullet.transform.position))
				{
					RemoveBullet(bullet);
				}
			}
		}

		public void FlyBulletByArgs(Args args)
		{
			var bullet = _bulletPool.Get().GetComponent<Bullet>();

			bullet.SetPosition(args.position);
			bullet.SetColor(args.color);
			bullet.SetPhysicsLayer(args.physicsLayer);
			bullet.damage = args.damage;
			bullet.isPlayer = args.isPlayer;
			bullet.SetVelocity(args.velocity);
			
			if (_activeBullets.Add(bullet))
			{
				bullet.OnCollisionEntered += OnBulletCollision;
			}
		}
		
		private void OnBulletCollision(Bullet bullet, Collision2D collision)
		{
			BulletUtils.DealDamage(bullet, collision.gameObject);
			RemoveBullet(bullet);
		}

		private void RemoveBullet(Bullet bullet)
		{
			if (_activeBullets.Remove(bullet))
			{
				bullet.OnCollisionEntered -= OnBulletCollision;
				_bulletPool.Return(bullet.gameObject);
			}
		}
		
		public struct Args
		{
			public Vector2 position;
			public Vector2 velocity;
			public Color color;
			public int physicsLayer;
			public int damage;
			public bool isPlayer;
		}
	}
}