using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
    public class UnitFactory
    {
        private Dictionary<string, Unit> _prefabs = new Dictionary<string, Unit>();
        public Unit FindPrefab(string type)
        {
            if (_prefabs == null || _prefabs.Count == 0)
                Initialise();
            if (_prefabs.Count == 0)
                throw new Exception("Could not find unit prefabs");

            if (_prefabs.TryGetValue(type, out Unit prefab))
            {
                return prefab;
            }

            return default;
        }

        private void Initialise()
        {
            var unitPrefabs = Resources.LoadAll<Unit>("");
            foreach (var unitPrefab in unitPrefabs)
            {
                _prefabs.Add(unitPrefab.Type, unitPrefab);
            }
        }
    }
}