using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	[CreateAssetMenu(fileName = "ScriptableObjectInstaller", menuName = "Installers/ScriptableObjectInstaller")]
	public class SCriptableObjectInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private InputConfig _inputConfig;
		[SerializeField] private BulletConfig _playerBulletConfig;
		
		public override void InstallBindings()
		{
			Container.Bind<BulletConfig>().FromInstance(_playerBulletConfig).AsSingle();

			Container.Bind<InputConfig>().FromInstance(_inputConfig).AsSingle();
		}
	}
}