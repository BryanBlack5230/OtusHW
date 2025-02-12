using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class HeroHelper : MonoBehaviour
    {
        [SerializeField] private HeroPopup2 _heroPopup;
        [SerializeField] private UserInfoSO _userInfoSO;
        [SerializeField] private CharacterInfoSO _characterInfoSO;

        private HeroPresenterFactory _heroPresenterFactory;

        [Inject]
        public void Construct(HeroPresenterFactory heroPresenterFactory)
        {
            _heroPresenterFactory = heroPresenterFactory;
        }

        [Button]
        private void ShowPopup()
        {
            _heroPopup.Show(_heroPresenterFactory.Create(_userInfoSO, _characterInfoSO));
        }
    }
}