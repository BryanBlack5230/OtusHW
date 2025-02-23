using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour, IHeroPart
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _valueText;
    public string Name => _nameText.text;
    
    private StatPresenter _presenter;

    public void Initialized(StatPresenter statPresenter)
    {
        _presenter = statPresenter;
        _presenter.OnNameChanged += OnNameChanged;
        _presenter.OnValueChanged += OnValueChanged;
        
        OnNameChanged(statPresenter.Name);
        OnValueChanged(statPresenter.Value);
    }

    private void OnValueChanged(int value)
    {
        _valueText.text = value.ToString();
    }

    private void OnNameChanged(string name)
    {
        _nameText.text = $"{name}: ";
    }

    public void Destroy()
    {
        _presenter.OnNameChanged -= OnNameChanged;
        _presenter.OnValueChanged -= OnValueChanged;
        
        Destroy(gameObject);
    }
}