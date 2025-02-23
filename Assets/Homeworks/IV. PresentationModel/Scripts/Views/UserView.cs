using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserView : MonoBehaviour, IHeroPart
{
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _description;
	
    private UserPresenter _userPresenter;
    public void Initialized(UserPresenter userPresenter)
    {
        _userPresenter = userPresenter;

        _userPresenter.OnNameChanged += OnNameChanged;
        _userPresenter.OnDescriptionChanged += OnDescriptionChanged;
        _userPresenter.OnIconChanged += OnIconChanged;
        
        _playerName.text = _userPresenter.Title;
        _description.text = _userPresenter.Description;
        _characterImage.sprite = _userPresenter.Icon;
    }

    private void OnIconChanged(Sprite icon)
    {
        _characterImage.sprite = icon;
    }

    private void OnDescriptionChanged(string description)
    {
        _description.text = description;
    }

    private void OnNameChanged(string name)
    {
        _playerName.text = name;
    }


    public void Destroy()
    {
        _userPresenter.OnNameChanged -= OnNameChanged;
        _userPresenter.OnDescriptionChanged -= OnDescriptionChanged;
        _userPresenter.OnIconChanged -= OnIconChanged;
        Destroy(gameObject);
    }
}