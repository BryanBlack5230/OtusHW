using UnityEngine;

namespace ShootEmUp
{
    public class CharacterShooterController: MonoBehaviour
	{
		[SerializeField] private BulletSystem _bulletSystem;
		[SerializeField] private BulletConfig _bulletConfig;
		[SerializeField] private InputManager _inputManager;
		[SerializeField] private WeaponComponent _weaponComponent;
		
		private void OnEnable()
		{
			_inputManager.OnFireEvent += OnFire;
		}

		private void OnDisable()
		{
			_inputManager.OnFireEvent -= OnFire;
		}
		
		private void OnFire()
		{
			_bulletSystem.Shoot(new BulletSystem.ShootArgs
			{
				isPlayer = true,
				physicsLayer = (int) _bulletConfig.physicsLayer,
				color = _bulletConfig.color,
				damage = _bulletConfig.damage,
				position = _weaponComponent.Position,
				velocity = _weaponComponent.Rotation * Vector3.up * _bulletConfig.speed
			});
		}
	}
}