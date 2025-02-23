using System;
using Lessons.Architecture.PM;

public class StatPresenter
{
    public Action<string> OnNameChanged;
    public Action<int> OnValueChanged;
    public string Name { get; set; }
    public int Value { get; set; }
    
    private CharacterStat _stat;
    public StatPresenter(CharacterStat stat)
    {
        _stat = stat;
        _stat.OnNameChanged += OnNameChanged_Presenter;
        _stat.OnValueChanged += OnValueChanged_Presenter;
        
        Name = _stat.Name;
        Value = _stat.Value;
    }

    private void OnValueChanged_Presenter(int value)
    {
        Value = value;
        OnValueChanged?.Invoke(value);
    }

    private void OnNameChanged_Presenter(string name)
    {
        Name = name;
        OnNameChanged?.Invoke(name);
    }

    ~StatPresenter()
    {
        OnNameChanged -= OnNameChanged_Presenter;
        OnValueChanged -= OnValueChanged_Presenter;
    }
}