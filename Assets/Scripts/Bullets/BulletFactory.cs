using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public interface IBulletFactory
	{
		GameObject Create();
	}

	public class BulletFactory : IBulletFactory
	{
		private readonly DiContainer _container;
		private readonly Bullet _bulletPrefab;

		public BulletFactory(DiContainer container, Bullet bulletPrefab)
		{
			_container = container;
			_bulletPrefab = bulletPrefab;
		}

		public GameObject Create()
		{
			return _container.InstantiatePrefab(_bulletPrefab.gameObject);
		}
	}
}