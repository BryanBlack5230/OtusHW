using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPopup : MonoBehaviour
{
    [SerializeField] private Transform _container; 
    [SerializeField] private Button _closeButton;
    [SerializeField] private UserView _userPrefab;
    [SerializeField] private LevelView _levelPrefab;
    [SerializeField] private StatsView _statsPrefab;
	
    private readonly List<IHeroPart> _parts = new();
    
    public void Show(HeroPopupPresenter heroPopupPresenter)
    {
        gameObject.SetActive(true);

        var userView = Instantiate(_userPrefab, _container);
        userView.Initialized(heroPopupPresenter.User);
        _parts.Add(userView);

        var levelView = Instantiate(_levelPrefab, _container);
        levelView.Initialized(heroPopupPresenter.Level);
        _parts.Add(levelView);
		
        var statsView = Instantiate(_statsPrefab, _container);
        statsView.Initialized(heroPopupPresenter.Stats);
        _parts.Add(statsView);
		
        _closeButton.onClick.AddListener(Hide);
    }
	
    private void Hide()
    {
        gameObject.SetActive(false);
        foreach (var part in _parts)
        {
            part.Destroy();
        }
        _parts.Clear();
        _closeButton.onClick.RemoveListener(Hide);
    }
}