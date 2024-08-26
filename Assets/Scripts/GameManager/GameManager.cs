using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField] private BulletSystem _bulletSystemPrefab;
		[SerializeField] private GameObject _enemyPrefab;

		public override void InstallBindings()
		{
			Container.Bind<GameLoopManager>().FromComponentInHierarchy().AsSingle();

			Container.Bind<BulletSystem>().FromComponentInNewPrefab(_bulletSystemPrefab).AsSingle();

			EnemyBindingsInstaller.Install(Container);
		}
	}
	
	public class EnemyBindingsInstaller : Installer<EnemyBindingsInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<WeaponComponent>().AsTransient();
			Container.Bind<MoveComponent>().AsTransient();
			Container.Bind<HitPointsComponent>().AsTransient();
			Container.Bind<EnemyMoveAgent>().AsTransient();
			Container.Bind<EnemyAttackAgent>().AsTransient();
		}
	}
	
	public sealed class GameManager : MonoBehaviour
	{
		[SerializeField] private GameLoopManager _gameLoopManager;
		[SerializeField] private GameManagerUIController _uiController;

		private void Start()
		{
			_uiController.SubscribeButtons(StartGame, PauseGame, ResumeGame);
		}

		public async void StartGame()
		{
			_uiController.OnStart();

			var countdown = new Countdown(3);
			await countdown.StartCountdownAsync();

			_gameLoopManager.StartGame();
		}

		public void PauseGame()
		{
			Debug.Log("Game paused!");
			_gameLoopManager.PauseGame();

			_uiController.OnPause();
		}

		public void ResumeGame()
		{
			Debug.Log("Game resumed!");
			_gameLoopManager.ResumeGame();

			_uiController.OnResume();
		}

		public void FinishGame()
		{
			Debug.Log("Game over!");
			_gameLoopManager.FinishGame();
		}
	}
}