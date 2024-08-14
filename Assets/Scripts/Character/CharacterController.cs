using UnityEngine;

namespace ShootEmUp
{
	public sealed class CharacterController : MonoBehaviour
	{
		[SerializeField] private GameObject _character; 
		[SerializeField] private GameManager _gameManager;
		[SerializeField] private BulletSystem _bulletSystem;
		[SerializeField] private BulletConfig _bulletConfig;
		[SerializeField] private InputManager _inputManager;
		private HitPointsComponent _charHP;
		private MoveComponent _charMC;
		private WeaponComponent _charWC;
		
		private void Awake() 
		{
			_charHP = _character.GetComponent<HitPointsComponent>();
			_charMC = _character.GetComponent<MoveComponent>();
			_charWC = _character.GetComponent<WeaponComponent>();
		}

		private void OnEnable()
		{
			_charHP.HpEmpty += OnCharacterDeath;
			_inputManager.OnFireEvent += OnFlyBullet;
		}

		private void OnDisable()
		{
			_charHP.HpEmpty -= OnCharacterDeath;
			_inputManager.OnFireEvent -= OnFlyBullet;
		}

		private void FixedUpdate() 
		{
			_charMC.MoveByRigidbodyVelocity(_inputManager.HorizontalDirection * Time.fixedDeltaTime);
		}

		private void OnCharacterDeath(GameObject _) => _gameManager.FinishGame();

		private void OnFlyBullet()
		{
			_bulletSystem.FlyBulletByArgs(new BulletSystem.Args
			{
				isPlayer = true,
				physicsLayer = (int) _bulletConfig.physicsLayer,
				color = _bulletConfig.color,
				damage = _bulletConfig.damage,
				position = _charWC.Position,
				velocity = _charWC.Rotation * Vector3.up * _bulletConfig.speed
			});
		}
	}
}