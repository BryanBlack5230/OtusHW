using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine;
using UnityEngine;

namespace SaveSystem
{
    public class ResourceData
    {
        public string ID;
        public int Amount;
    }
    
    public class ResourceSaveLoader : SaveLoader<ResourceService, ResourceData[]>
    {
        public ResourceSaveLoader(ResourceService service) : base(service){}
        protected override ResourceData[] ConvertToData()
        {
            var list = new List<ResourceData>();
            foreach (var resource in base.service.GetResources())
            {
                var data = new ResourceData()
                {
                    ID = resource.ID,
                    Amount = resource.Amount
                };
                list.Add(data);
            }
            
            var builder = new StringBuilder();
            builder.AppendLine($"Converting Resources({list.Count}) to Data\n");
            foreach (var data in list)
            {
                builder.AppendLine(ToString(data));
            }
            builder.AppendLine("Completed");
            Debug.Log(builder.ToString());
            return list.ToArray();
        }

        protected override void SetupData(ResourceData[] resourceData)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Setting up Resources({resourceData.Length}) to GameObjects({base.service.GetResources().Count()})\n");
            foreach (var resource in base.service.GetResources())
            {
                foreach (var data in resourceData)
                {
                    if (resource.ID == data.ID)
                    {
                        builder.AppendLine($"Matched ({resource.ID}, {data.ID}), setting Amount to : {data.Amount}\n");
                        resource.Amount = data.Amount;
                    }
                }
            }
            builder.AppendLine("Completed");
            Debug.Log(builder.ToString());
        }

        private string ToString(ResourceData data)
        {
            return $"ID: {data.ID}, Amount: {data.Amount}\n";
        }
    }
}