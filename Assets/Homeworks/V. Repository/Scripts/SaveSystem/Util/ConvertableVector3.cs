using System;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class ConvertableVector3
    {
        public float[] Value { get; private set; }
        
        [JsonConstructor]
        public ConvertableVector3(float[] value)
        {
            Value = value ?? new float[3];
        }
        public ConvertableVector3(Vector3 vector)
        {
            Value = new[] { vector.x, vector.y, vector.z };
        }

        public Vector3 ToVector3()
        {
            return new Vector3(Value[0], Value[1], Value[2]);
        }
    }
}