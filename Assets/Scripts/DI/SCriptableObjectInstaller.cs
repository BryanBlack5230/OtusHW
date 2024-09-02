using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class SCriptableObjectInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private InputConfig _inputConfig;
	}
}