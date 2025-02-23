using System.Collections.Generic;
using UnityEngine;

public class StatsView : MonoBehaviour, IHeroPart
{
    [SerializeField] private StatView _statViewPrefab;
    [SerializeField] private Transform _statsContainer;
    
    private List<StatView> _statViews = new();
    private StatsPresenter _statsPresenter;
    public void Initialized(StatsPresenter statsPresenter)
    {
        _statsPresenter = statsPresenter;
        
        _statsPresenter.OnStatRemoved += OnStatRemoved;
        _statsPresenter.OnStatAdded += OnStatAdded;
        
        foreach (var presenter in _statsPresenter.StatPresenters)
        {
            OnStatAdded(presenter);
        }
    }

    private void OnStatRemoved(string name)
    {
        foreach (var view in _statViews)
        {
            if (view.Name.Equals(name))
            {
                view.Destroy();
                _statViews.Remove(view);
                return;
            }
        }
    }

    private void OnStatAdded(StatPresenter presenter)
    {
        var statView = Instantiate(_statViewPrefab, _statsContainer);
        statView.Initialized(presenter);
        _statViews.Add(statView);
    }


    public void Destroy()
    {
        _statsPresenter.OnStatRemoved -= OnStatRemoved;
        _statsPresenter.OnStatAdded -= OnStatAdded;
        
        foreach (var view in _statViews)
        {
            view.Destroy();
        }
        
        _statViews.Clear();
        Destroy(gameObject);
    }
}