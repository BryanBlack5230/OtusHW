using System;
using System.Linq;
using Lessons.Architecture.PM;

public class StatsPresenter
{
    public CharacterStat[] Stats => _characterInfo.GetStats().ToArray();
    public Action<CharacterStat> OnStatAdded; 
    public Action<CharacterStat> OnStatRemoved; 
    
    private readonly CharacterInfo _characterInfo;

    public StatsPresenter(CharacterInfo characterInfo)
    {
        _characterInfo = characterInfo;
    }

    ~StatsPresenter()
    {
        
    }
}