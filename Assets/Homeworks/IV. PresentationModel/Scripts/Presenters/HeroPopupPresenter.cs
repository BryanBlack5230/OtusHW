public class HeroPopupPresenter
{
    public UserPresenter User { get; set; }
    public LevelPresenter Level { get; set; }
    public StatsPresenter Stats { get; set; }

    public HeroPopupPresenter(UserPresenter userPresenter, LevelPresenter levelPresenter, StatsPresenter statsPresenter)
    {
        User = userPresenter;
        Level = levelPresenter;
        Stats = statsPresenter;
    }
}