using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class CharacterMoveController: MonoBehaviour, IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener, IGameFixedUpdateListener
	{
		[SerializeField] private MoveComponent _moveComponent;
		private InputManager _inputManager;

		[Inject]
		public void Construct(InputManager inputManager)
		{
			_inputManager = inputManager;
		}
		private void Awake() 
		{
			IGameListener.Register(this);
		}

		public void OnFinishGame()
		{
		}

		public void OnFixedUpdate(float fixedDeltaTime)
		{
			_moveComponent.Move(_inputManager.HorizontalDirection * fixedDeltaTime);
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