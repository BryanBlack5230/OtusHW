using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LockableButton : MonoBehaviour
{
    [FormerlySerializedAs("_levelUpButton")] [SerializeField] private Button _button;
    [SerializeField] private Image _buttonBackground;
    
    [SerializeField] private Sprite _availableSprite;
    [SerializeField] private Sprite _lockedSprite;
    [SerializeField] private ButtonState _state;
    public Button Button => _button;
    public void AddListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }

    public void SetAvailable(bool isAvailable)
    {
        var state = isAvailable ? ButtonState.Available : ButtonState.Locked;
        SetState(state);
    }

    public void SetState(ButtonState state)
    {
        _state = state;

        switch (_state)
        {
            case ButtonState.Available:
                Button.interactable = true;
                _buttonBackground.sprite = _availableSprite;
                break;
            case ButtonState.Locked:
                Button.interactable = false;
                _buttonBackground.sprite = _lockedSprite;
                break;
            default:
                Button.interactable = false;
                _buttonBackground.sprite = _lockedSprite;
                break;
        }
    }
}

public enum ButtonState
{
    Unknown,
    Locked,
    Available
}