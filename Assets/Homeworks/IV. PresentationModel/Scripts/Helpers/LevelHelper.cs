using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class LevelHelper : MonoBehaviour
    {
        [ShowInInspector] private PlayerLevel _playerLevel;

        [Inject]
        private void Constructor(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }
    }
}