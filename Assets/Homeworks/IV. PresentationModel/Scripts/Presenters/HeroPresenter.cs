using System;
using Lessons.Architecture.PM;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

public class HeroPresenter
{
    public bool CanLevelUp => _playerLevel.CanLevelUp();
    public string Description { get; }
    public Sprite Icon { get; }
    public string Name { get;}
    public int RequiredExperience { get; private set; }
    public int CurrentExperience { get; private set; }
    public string CurrentLevel { get; private set; }
    public CharacterStat[] Stats => _playerStats.ToArray();
    private HashSet<CharacterStat> _playerStats;

    private readonly PlayerLevel _playerLevel;

    public HeroPresenter(CharacterInfo characterInfo, UserInfo userInfo, PlayerLevel playerLevel)
    {
        _playerLevel = playerLevel;

        Description = userInfo.Description;
        Icon = userInfo.Icon;
        Name = userInfo.Name;

        SetExperienceData();

        _playerStats = new HashSet<CharacterStat>(characterInfo.GetStats());

        _playerLevel.OnExperienceChanged += OnExperienceChanged;
        _playerLevel.OnLevelUp += OnLevelUp;
        characterInfo.OnStatAdded += OnStatAdded;
        characterInfo.OnStatRemoved += OnStatRemoved;
    }

    private void OnStatRemoved(CharacterStat stat)
    {
        _playerStats.Remove(stat);
    }

    private void OnStatAdded(CharacterStat stat)
    {
        _playerStats.Add(stat);
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

    private void SetExperienceData()
    {
        RequiredExperience = _playerLevel.RequiredExperience;
        CurrentExperience = _playerLevel.CurrentExperience;
        CurrentLevel = $"Level: {_playerLevel.CurrentLevel.ToString()}";
    }

    private void OnLevelUp()
    {
        SetExperienceData();
    }

    private void OnExperienceChanged(int experience)
    {
        CurrentExperience = experience;
    }
}