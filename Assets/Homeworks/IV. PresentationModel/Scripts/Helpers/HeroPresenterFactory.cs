namespace Lessons.Architecture.PM
{
    public class HeroPresenterFactory
    {
        private PlayerLevel _playerLevel;
        private UserInfo _userInfo;
        private CharacterInfo _characterInfo;

        public HeroPresenterFactory(PlayerLevel playerLevel, UserInfo userInfo, CharacterInfo characterInfo)
        {
            _playerLevel = playerLevel;
            _userInfo = userInfo;
            _characterInfo = characterInfo;
        }

        public HeroPopupPresenter Create(UserInfoSO userInfoSO, CharacterInfoSO characterInfoSO)
        {
            SetUser(userInfoSO);
            SetCharacter(characterInfoSO);
            
            var level = new LevelPresenter(_playerLevel);
            var user = new UserPresenter(_userInfo);
            var stats = new StatsPresenter(_characterInfo);

            return new HeroPopupPresenter(user, level, stats);
        }
        
        private void SetCharacter(CharacterInfoSO characterInfoSO)
        {
            var stats = _characterInfo.GetStats();

            foreach (var stat in stats)
            {
                _characterInfo.RemoveStat(stat);
            }

            foreach (var stat in characterInfoSO.GetStats())
            {
                _characterInfo.AddStat(stat);
            }
        }

        private void SetUser(UserInfoSO userInfoSO)
        {
            _userInfo.ChangeName(userInfoSO.Name);
            _userInfo.ChangeDescription(userInfoSO.Description);
            _userInfo.ChangeIcon(userInfoSO.Icon);
        }
    }
}