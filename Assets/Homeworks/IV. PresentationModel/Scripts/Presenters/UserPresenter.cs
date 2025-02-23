using System;
using Lessons.Architecture.PM;
using UnityEngine;

public class UserPresenter
{
    public Action<string> OnNameChanged;
    public Action<string> OnDescriptionChanged;
    public Action<Sprite> OnIconChanged;
    public string Title { get; set; }
    public string Description { get; set; }
    public Sprite Icon { get; set; }

    private UserInfo _userInfo;

    public UserPresenter(UserInfo userInfo)
    {
        _userInfo = userInfo;
        _userInfo.OnNameChanged += OnNameChanged_Presenter;
        _userInfo.OnDescriptionChanged += OnDescriptionChanged_Presenter;
        _userInfo.OnIconChanged += OnIconChanged_Presenter;
        
        Title = userInfo.Name;
        Description = userInfo.Description;
        Icon = userInfo.Icon;
    }

    private void OnDescriptionChanged_Presenter(string description)
    {
        Description = description;
        OnDescriptionChanged?.Invoke(description);
    }

    private void OnIconChanged_Presenter(Sprite icon)
    {
        Icon = icon;
        OnIconChanged?.Invoke(icon);
    }

    private void OnNameChanged_Presenter(string name)
    {
        Title = name;
        OnNameChanged?.Invoke(name);
    }

    ~UserPresenter()
    {
        _userInfo.OnNameChanged -= OnNameChanged_Presenter;
        _userInfo.OnDescriptionChanged -= OnDescriptionChanged_Presenter;
        _userInfo.OnIconChanged -= OnIconChanged_Presenter;
    }
}