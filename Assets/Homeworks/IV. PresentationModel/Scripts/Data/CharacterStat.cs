using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [System.Serializable]
    public sealed class CharacterStat
    {
        public event Action<int> OnValueChanged; 
        public event Action<string> OnNameChanged; 

        [ShowInInspector, ReadOnly]
        public string Name { get; private set; }

        [ShowInInspector, ReadOnly]
        public int Value { get; private set; }

        [Button]
        public void ChangeValue(int value)
        {
            this.Value = value;
            this.OnValueChanged?.Invoke(value);
        }
        
        [Button]
        public void ChangeName(string name)
        {
            this.Name = name;
            this.OnNameChanged?.Invoke(name);
        }
    }
}