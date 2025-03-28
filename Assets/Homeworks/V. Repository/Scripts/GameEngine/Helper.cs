using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameEngine
{
    public class Helper : MonoBehaviour
    {
        [ShowInInspector] private ResourceService _resourceService;
        [ShowInInspector] private UnitManager _unitManager;
        
        [Inject]
        public void Construct(ResourceService resourceService, UnitManager unitManager)
        {
            _resourceService = resourceService;
            _unitManager = unitManager;
        }
    }
}