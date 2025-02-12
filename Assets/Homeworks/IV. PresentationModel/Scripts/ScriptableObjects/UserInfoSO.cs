using UnityEngine;

namespace Lessons.Architecture.PM
{
    [CreateAssetMenu(fileName = "UserInfoSO", menuName = "Configs/UserInfo")]
    public class UserInfoSO : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;
    }
}