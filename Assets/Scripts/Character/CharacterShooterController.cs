using UnityEngine;

namespace ShootEmUp
{
	public class CharacterShooterController: MonoBehaviour, IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener
	{
		[SerializeField] private BulletSystem _bulletSystem;
		[SerializeField] private BulletConfig _bulletConfig;
		[SerializeField] private InputManager _inputManager;
		[SerializeField] private WeaponComponent _weaponComponent;
		
		private void Awake() 
		{
			IGameListener.Register(this);
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