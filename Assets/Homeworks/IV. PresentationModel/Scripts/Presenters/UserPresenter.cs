using Lessons.Architecture.PM;
using UnityEngine;

public class UserPresenter
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Sprite Icon { get; set; }

    public UserPresenter(UserInfo userInfo)
    {
        Title = userInfo.Name;
        Description = userInfo.Description;
        Icon = userInfo.Icon;
    }
}