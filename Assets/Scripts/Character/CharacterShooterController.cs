using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class CharacterShooterController: MonoBehaviour, IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener
	{
		[SerializeField] private WeaponComponent _weaponComponent;
		private BulletSystem _bulletSystem;
		private BulletConfig _bulletConfig;
		private InputManager _inputManager;
		
		[Inject]
		public void Construct(BulletSystem bulletSystem, BulletConfig bulletConfig, InputManager inputManager)
		{
			_bulletSystem = bulletSystem;
			_bulletConfig = bulletConfig;
			_inputManager = inputManager;
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

		public void OnResume()
		{
			_inputManager.OnFireEvent += OnFire;
		}

		public void OnPause()
		{
			_inputManager.OnFireEvent -= OnFire;
		}

		public void OnFinishGame()
		{
			_inputManager.OnFireEvent -= OnFire;
		}

		public void OnStartGame()
		{
			_inputManager.OnFireEvent += OnFire;
		}
	}
}