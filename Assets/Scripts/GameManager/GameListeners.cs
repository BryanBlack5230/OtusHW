using System;

namespace ShootEmUp
{
	public interface IGameListener
	{
		public static event Action<IGameListener> OnRegister;
		
		public static void Register(IGameListener gameListener)
		{
			OnRegister?.Invoke(gameListener);
		}
	}
	
	public interface IGameStartListener : IGameListener
	{
		void OnStartGame();
	}
	
	public interface IGameFinishListener : IGameListener
	{
		void OnFinishGame();
	}
	
	public interface IGamePauseListener : IGameListener
	{
		void OnPause();
	}
	
	public interface IGameResumeListener : IGameListener
	{
		void OnResume();
	}

	public interface IGameUpdateListener : IGameListener
	{
		void OnUpdate(float deltaTime);
	}
	
	public interface IGameFixedUpdateListener : IGameListener
	{
		void OnFixedUpdate(float fixedDeltaTime);
	}
	
	public interface IGameLateUpdateListener : IGameListener
	{
		void OnLateUpdate(float deltaTime);
	}
}
