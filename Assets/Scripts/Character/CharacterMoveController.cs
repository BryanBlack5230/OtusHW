using UnityEngine;

namespace ShootEmUp
{
    public class CharacterMoveController: MonoBehaviour
	{
		[SerializeField] private MoveComponent _moveComponent;
		[SerializeField] private InputManager _inputManager;
		
		private void FixedUpdate()
		{
			_moveComponent.MoveByRigidbodyVelocity(_inputManager.HorizontalDirection * Time.fixedDeltaTime);
		}
	}
}