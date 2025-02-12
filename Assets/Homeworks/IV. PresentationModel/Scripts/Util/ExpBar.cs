using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ExpBar 
{
	[SerializeField] private Slider _slider;
	[SerializeField] private TextMeshProUGUI _text;
	[SerializeField] private Sprite _fullBarSprite; 
	[SerializeField] private Sprite _defaultBarSprite; 

	public void SetMax(int requiredExperience)
	{
		_slider.maxValue = requiredExperience;
	}

	public void SetCurrent(int currentExperience)
	{
		_slider.value = currentExperience;
	}

	private void UpdateSprite()
	{
		var sprite = _slider.value >= _slider.maxValue ? _fullBarSprite : _defaultBarSprite;
		_slider.image.sprite = sprite;
	}

	private void UpdateText()
	{
		_text.text = $"{_slider.value.ToString()} / {_slider.maxValue.ToString()}";
	}

	public void UpdateState()
	{
		UpdateSprite();
		UpdateText();
	}
}
