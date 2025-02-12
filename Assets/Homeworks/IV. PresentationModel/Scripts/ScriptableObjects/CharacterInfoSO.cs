using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [CreateAssetMenu(fileName = "CharacterInfoSO", menuName = "Configs/CharacterInfo")]
    public class CharacterInfoSO : ScriptableObject
    {
        [SerializeField] private CharacterStat[] stats;
        public  HashSet<CharacterStat> GetStats() => new HashSet<CharacterStat>(stats);
    }
}