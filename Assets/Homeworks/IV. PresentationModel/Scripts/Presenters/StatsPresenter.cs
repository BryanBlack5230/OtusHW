using System;
using System.Collections.Generic;
using Lessons.Architecture.PM;

public class StatsPresenter
{
    public Action<StatPresenter> OnStatAdded; 
    public Action<string> OnStatRemoved;

    public List<StatPresenter> StatPresenters => _presenters;
    private List<StatPresenter> _presenters = new();
    private CharacterInfo _characterInfo;

    public StatsPresenter(CharacterInfo characterInfo)
    {
        _characterInfo = characterInfo;
        _characterInfo.OnStatAdded += OnStatAdded_Presenter;
        _characterInfo.OnStatRemoved += OnStatRemoved_Presenter;
        
        CreatePresenters();
    }

    private void CreatePresenters()
    {
        var stats = _characterInfo.GetStats();

        foreach (var stat in stats)
        {
            var presenter = new StatPresenter(stat);
            _presenters.Add(presenter);
        }
    }

    private void OnStatRemoved_Presenter(CharacterStat stat)
    {
        OnStatRemoved?.Invoke(stat.Name);
        var changedStatIndex = SearchStatPresenter(stat);
        if (changedStatIndex == -1) throw new ArgumentException($"Could not find stat presenter for {stat.Name}.");
        _presenters.RemoveAt(changedStatIndex);
    }

    private int SearchStatPresenter(CharacterStat stat)
    {
        var count = -1;

        foreach (var presenter in _presenters)
        {
            count++;
            if (presenter.Name.Equals(stat.Name))
                return count;
        }

        return -1;
    }

    private void OnStatAdded_Presenter(CharacterStat stat)
    {
        var presenter = new StatPresenter(stat);
        _presenters.Add(presenter);
        
        OnStatAdded?.Invoke(presenter);
    }

    ~StatsPresenter()
    {
        _characterInfo.OnStatAdded -= OnStatAdded_Presenter;
        _characterInfo.OnStatRemoved -= OnStatRemoved_Presenter;
    }
}