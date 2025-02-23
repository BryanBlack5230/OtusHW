using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class HeroHelper : MonoBehaviour
    {
        [SerializeField] private HeroPopup _heroPopup;
        [SerializeField] private UserInfoSO _userInfoSO;
        [SerializeField] private CharacterInfoSO _characterInfoSO;

        private HeroPresenterFactory _heroPresenterFactory;
        [ShowInInspector] private PlayerLevel _playerLevel;
        [ShowInInspector] private CharacterInfo _characterInfo;
        [ShowInInspector] private UserInfo _userInfo;
        
        [Inject]
        public void Construct(PlayerLevel playerLevel, CharacterInfo characterInfo, UserInfo userInfo, HeroPresenterFactory heroPresenterFactory)
        {
            _heroPresenterFactory = heroPresenterFactory;
            
            _playerLevel = playerLevel;
            _characterInfo = characterInfo;
            _userInfo = userInfo;
        }

        [Button]
        private void ShowPopup()
        {
            _heroPopup.Show(_heroPresenterFactory.Create(_userInfoSO, _characterInfoSO));
        }
    }
}