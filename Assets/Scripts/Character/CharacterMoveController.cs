using UnityEngine;

namespace ShootEmUp
{
	public class CharacterMoveController: MonoBehaviour, IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener, IGameFixedUpdateListener
	{
		[SerializeField] private MoveComponent _moveComponent;
		[SerializeField] private InputManager _inputManager;

		private void Awake() 
		{
			IGameListener.Register(this);
		}

		public void OnFinishGame()
		{
		}

		public void OnFixedUpdate(float fixedDeltaTime)
		{
			_moveComponent.MoveByRigidbodyVelocity(_inputManager.HorizontalDirection * fixedDeltaTime);
		}

		public void OnPause()
		{
		}


		public void OnResume()
		{
		}

		public void OnStartGame()
		{
		}
	}
}