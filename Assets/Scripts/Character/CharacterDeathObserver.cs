using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class CharacterDeathObserver: IGameStartListener, IGameFinishListener, IGamePauseListener, IGameResumeListener
	{
		private HitPointsComponent _hitPointsComponent;
		private GameManager _gameManager;

		public CharacterDeathObserver(GameManager gameManager, [Inject(Id = "Player")] Transform character)
		{
			_gameManager = gameManager;
			_hitPointsComponent = character.GetComponent<HitPointsComponent>();
		}
		
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