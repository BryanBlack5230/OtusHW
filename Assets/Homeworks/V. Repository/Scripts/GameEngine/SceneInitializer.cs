using UnityEngine;

namespace GameEngine
{
    public class SceneInitializer
    {
        public SceneInitializer(ResourceService resourceService, Resource[] existingResources, 
                                UnitManager unitManager, Unit[] existingUnits, Transform unitsContainer)
        {
            resourceService.SetResources(existingResources);
            unitManager.SetupUnits(existingUnits);
            unitManager.SetContainer(unitsContainer);
        }        
    }
}