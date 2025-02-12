using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour, IHeroPart
{
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private LockableButton _levelUpButton;
    [SerializeField] private ExpBar _experience;

    private LevelPresenter _levelPresenter;
    public void Initialized(LevelPresenter levelPresenter)
    {
        _levelPresenter = levelPresenter;
        _level.text = _levelPresenter.CurrentLevel;
        _levelPresenter.OnLevelUp += OnLevelUp;
        _levelPresenter.OnExperienceChanged += OnExperienceChanged;
        _levelUpButton.AddListener(_levelPresenter.LevelUp);
        UpdateButtonState();
        UpdateExperienceBar();
    }

    private void OnExperienceChanged(int _)
    {
        UpdateExperienceBar();
        UpdateButtonState();
    }

    private void OnLevelUp()
    {
        _level.text = _levelPresenter.CurrentLevel;
        UpdateExperienceBar();
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        var buttonState = _levelPresenter.CanLevelUp
            ? ButtonState.Available
            : ButtonState.Locked;
        _levelUpButton.SetState(buttonState);
    }

    private void UpdateExperienceBar()
    {
        _experience.SetMax(_levelPresenter.RequiredExperience);
        _experience.SetCurrent(_levelPresenter.CurrentExperience);
        _experience.UpdateState();
    }
	
    public void Destroy()
    {
        _levelPresenter.OnLevelUp -= OnLevelUp;
        _levelPresenter.OnExperienceChanged -= OnExperienceChanged;
        _levelUpButton.RemoveListener(_levelPresenter.LevelUp);
        Destroy(gameObject);
    }
}