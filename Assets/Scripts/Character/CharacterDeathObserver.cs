using UnityEngine;

namespace ShootEmUp
{
	public class CharacterDeathObserver: MonoBehaviour, IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener
	{
		[SerializeField] private HitPointsComponent _hitPointsComponent;
		[SerializeField] private GameManager _gameManager;
		
		private void OnCharacterDeath(GameObject _) => _gameManager.FinishGame();

		public void OnStartGame()
		{
			_hitPointsComponent.HpEmpty += OnCharacterDeath;
		}

		public void OnFinishGame()
		{
			_hitPointsComponent.HpEmpty -= OnCharacterDeath;
		}

		public void OnPause()
		{
			_hitPointsComponent.HpEmpty -= OnCharacterDeath;
		}

		public void OnResume()
		{
			_hitPointsComponent.HpEmpty += OnCharacterDeath;
		}
	}
}