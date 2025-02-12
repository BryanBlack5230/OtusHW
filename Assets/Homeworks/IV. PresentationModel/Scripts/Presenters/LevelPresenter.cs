using System;
using Lessons.Architecture.PM;

public class LevelPresenter
{
    public int RequiredExperience { get; set; }
    public int CurrentExperience { get; set; }
    public string CurrentLevel { get; set; }
    public bool CanLevelUp => _playerLevel.CanLevelUp();
    public Action OnLevelUp;
    public Action<int> OnExperienceChanged;
    
    private PlayerLevel _playerLevel;

    public LevelPresenter(PlayerLevel playerLevel)
    {
        _playerLevel = playerLevel;
        RequiredExperience = _playerLevel.RequiredExperience;
        CurrentExperience = _playerLevel.CurrentExperience;
        CurrentLevel = $"Level: {_playerLevel.CurrentLevel.ToString()}";

        _playerLevel.OnLevelUp += LevelUp_Presenter;
        _playerLevel.OnExperienceChanged += ExperienceChanged_Presenter;
    }

    public void LevelUp()
    {
        if (CanLevelUp)
        {
            _playerLevel.LevelUp();
        }
        else
        {
            throw new Exception("You cannot level up");
        }
    }

    private void LevelUp_Presenter()
    {
        RequiredExperience = _playerLevel.RequiredExperience;
        CurrentExperience = _playerLevel.CurrentExperience;
        CurrentLevel = $"Level: {_playerLevel.CurrentLevel.ToString()}";
        OnLevelUp?.Invoke();
    }

    private void ExperienceChanged_Presenter(int xp)
    {
        CurrentExperience = _playerLevel.CurrentExperience;
        OnExperienceChanged?.Invoke(xp);
    }

    ~LevelPresenter()
    {
        _playerLevel.OnLevelUp -= LevelUp_Presenter;
        _playerLevel.OnExperienceChanged -= ExperienceChanged_Presenter;
    }
}