using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine;
using UnityEngine;

namespace SaveSystem
{
    public struct UnitData
    {
        public string Name;
        public ConvertableVector3 Position;
        public ConvertableVector3 Rotation;
        public string Type;
        public int HitPoints;
    }

    public class UnitSaveLoader : SaveLoader<UnitManager, UnitData[]>
    {
        private readonly UnitFactory _factory;
        public UnitSaveLoader(UnitManager service, UnitFactory factory) : base(service) {_factory = factory;}
        
        protected override UnitData[] ConvertToData()
        {
            var list = new List<UnitData>();
            foreach (var unit in base.service.GetAllUnits())
            {
                var transform = unit.transform;
                
                var data = new UnitData()
                {
                    Name = unit.name,
                    Position = new(transform.position),
                    Rotation = new(transform.rotation.eulerAngles),
                    Type = unit.Type,
                    HitPoints = unit.HitPoints
                };
                list.Add(data);
            }
            
            var builder = new StringBuilder();
            builder.AppendLine($"Converting Units({list.Count}) to Data\n");
            foreach (var data in list)
            {
                builder.AppendLine(ToString(data));
            }
            builder.AppendLine("Completed");
            Debug.Log(builder.ToString());
            return list.ToArray();
        }
        
        protected override void SetupData(UnitData[] unitData)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Setting up Units({unitData.Length}) to GameObjects({base.service.GetAllUnits().Count()})\n");
            foreach (var data in unitData)
            {
                if (!TryUpdateUnit(data, builder))
                    CreateNewUnit(data, builder);
            }
            builder.AppendLine("Completed");
            Debug.Log(builder.ToString());
        }

        private void CreateNewUnit(UnitData data, StringBuilder builder)
        {
            var prefab = _factory.FindPrefab(data.Type);
            var rotation = Quaternion.Euler(data.Rotation.ToVector3());
            var unit = base.service.SpawnUnit(prefab, data.Position.ToVector3(), rotation);
            unit.name = data.Name;
            builder.Append($"<color=green>Spawned</color> Unit [{data.Type}] with HP[{data.HitPoints}] at Position [{data.Position.Value[0]},{data.Position.Value[1]},{data.Position.Value[2]}]\n");
        }

        private bool TryUpdateUnit(UnitData data, StringBuilder builder)
        {
            var updated = false;
            foreach (var unit in base.service.GetAllUnits())
            {
                if (unit.name.Equals(data.Name))
                {
                    updated = true;
                        
                    builder.AppendLine($"<color=yellow>Matched</color> ({unit.name}, {data.Name}), setting HP to : {data.HitPoints}, moving to Position [{data.Position.Value[0]},{data.Position.Value[1]},{data.Position.Value[2]}]\n");
                    unit.HitPoints = data.HitPoints;
                    unit.transform.position = data.Position.ToVector3();
                    var rotation = Quaternion.Euler(data.Rotation.ToVector3());
                    unit.transform.rotation = rotation;
                }
            }

            return updated;
        }

        private string ToString(UnitData data)
        {
            return $"Type: {data.Type}, HP: {data.HitPoints}, saved at Position [{data.Position.Value[0]},{data.Position.Value[1]},{data.Position.Value[2]}] and by name '{data.Name}']\n";
        }
    }
}