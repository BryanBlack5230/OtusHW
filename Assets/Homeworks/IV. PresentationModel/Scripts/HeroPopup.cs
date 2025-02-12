using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroPopup : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _playerName;
	[SerializeField] private Image _characterImage;
	[SerializeField] private TextMeshProUGUI _description;
	[SerializeField] private TextMeshProUGUI _level;
	[SerializeField] private LockableButton _levelUpButton;
	[SerializeField] private Button _closeButton;
	[SerializeField] private ExpBar _experience;
	[SerializeField] private TextMeshProUGUI[] _stats;
	[SerializeField] private StatsView _statsView;

	private HeroPresenter _heroPresenter;

	public void Show(HeroPresenter presenter)
	{
		gameObject.SetActive(true);
		
		_heroPresenter = presenter;

		_description.text = _heroPresenter.Description;
		_characterImage.sprite = _heroPresenter.Icon;
		_playerName.text = _heroPresenter.Name;
		
		_levelUpButton.AddListener(OnLevelUpButtonClicked);
		_closeButton.onClick.AddListener(Hide);
		UpdateButtonState();
		UpdateExperience();
		_statsView.UpdateStats(_heroPresenter.Stats);
	}

	private void UpdateButtonState()
	{
		var buttonState = _heroPresenter.CanLevelUp
			? ButtonState.Available
			: ButtonState.Locked;
		_levelUpButton.SetState(buttonState);
	}

	private void UpdateExperience()
	{
		_experience.SetMax(_heroPresenter.RequiredExperience);
		_experience.SetCurrent(_heroPresenter.CurrentExperience);
		_experience.UpdateState();

		_level.text = _heroPresenter.CurrentLevel;
	}
	
	private void OnLevelUpButtonClicked()
	{
		if (_heroPresenter.CanLevelUp)
		{
			_heroPresenter.LevelUp();
			UpdateExperience();
			UpdateButtonState();
		}
	}

	private void Hide()
	{
		gameObject.SetActive(false);
		
		_levelUpButton.RemoveListener(OnLevelUpButtonClicked);
		_closeButton.onClick.RemoveListener(Hide);
	}
}