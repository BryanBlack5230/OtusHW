using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserView : MonoBehaviour, IHeroPart
{
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _description;
	
    public void Initialized(UserPresenter userPresenter)
    {
        _playerName.text = userPresenter.Title;
        _description.text = userPresenter.Description;
        _characterImage.sprite = userPresenter.Icon;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}