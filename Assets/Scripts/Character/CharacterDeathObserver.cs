using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver: MonoBehaviour
	{
		[SerializeField] private HitPointsComponent _hitPointsComponent;
		[SerializeField] private GameManager _gameManager;
		
		private void OnEnable()
		{
			_hitPointsComponent.HpEmpty += OnCharacterDeath;
		}

		private void OnDisable()
		{
			_hitPointsComponent.HpEmpty -= OnCharacterDeath;
		}

		private void OnCharacterDeath(GameObject _) => _gameManager.FinishGame();
	}
}