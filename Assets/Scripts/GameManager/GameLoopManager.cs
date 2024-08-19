using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp
{
	public enum State
	{
		Unknown,
		Start,
		Finish,
		Pause,
		Resume
	}
	
	public class GameLoopManager : MonoBehaviour
	{
		private List<IGameListener> _listeners = new();
		private List<IGameUpdateListener> _updateListeners = new();
		private List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
		private List<IGameLateUpdateListener> _lateUpdateListeners = new();
		
		private State _state = State.Unknown;
		
		private void Awake()
		{
			IGameListener.OnRegister += OnRegister;
		}
		
		private void OnDestroy()
		{
			IGameListener.OnRegister -= OnRegister;
		}
		
		private void OnRegister(IGameListener gameListener)
		{
			_listeners.Add(gameListener);
			
			RegisterListener(gameListener);
			// RegisterListenerOld(gameListener);
		}
		
		private void RegisterListener(IGameListener gameListener)
		{
			RegisterListenerOfType<IGameUpdateListener>(ref _updateListeners, gameListener);
			RegisterListenerOfType<IGameFixedUpdateListener>(ref _fixedUpdateListeners, gameListener);
			RegisterListenerOfType<IGameLateUpdateListener>(ref _lateUpdateListeners, gameListener);
		}
		
		private void RegisterListenerOld(IGameListener gameListener)
		{
			if (gameListener is IGameUpdateListener gameUpdateListener)
			{
				_updateListeners.Add(gameUpdateListener);
			}
			
			if (gameListener is IGameFixedUpdateListener fixedUpdateListener)
			{
				_fixedUpdateListeners.Add(fixedUpdateListener);
			}
			
			if (gameListener is IGameLateUpdateListener lateUpdateListener)
			{
				_lateUpdateListeners.Add(lateUpdateListener);
			}
		}

		private void RegisterListenerOfType<T>(ref List<T> list, IGameListener gameListener) where T : class
		{
			if (gameListener is T listener)
			{
				list.Add(listener);
			}
		}

		
		private bool CanUpdate()
		{
			return _state is State.Start or State.Resume;
		}
		
		private void Update() 
		{
			if (!CanUpdate()) return;
			
			var deltaTime = Time.deltaTime;
			
			for (int i = 0; i < _updateListeners.Count; i++)
			{
				_updateListeners[i].OnUpdate(deltaTime);
			}
		}
		
		private void FixedUpdate() 
		{
			if (!CanUpdate()) return;
			
			var fixedDeltaTime = Time.fixedDeltaTime;
			
			for (int i = 0; i < _fixedUpdateListeners.Count; i++)
			{
				_fixedUpdateListeners[i].OnFixedUpdate(fixedDeltaTime);
			}
		}
		
		private void LateUpdate() 
		{
			if (!CanUpdate()) return;
			
			var deltaTime = Time.deltaTime;
			
			for (int i = 0; i < _lateUpdateListeners.Count; i++)
			{
				_lateUpdateListeners[i].OnLateUpdate(deltaTime);
			}
		}
		
		public void StartGame()
		{
			_state = State.Start;
			
			foreach (var gameListener in _listeners)
			{
				if (gameListener is IGameStartListener gameStartListener)
				{
					gameStartListener.OnStartGame();
				}
			}
		}
		
		public void FinishGame()
		{
			_state = State.Finish;
			
			foreach (var gameListener in _listeners)
			{
				if (gameListener is IGameFinishListener gameFinishListener)
				{
					gameFinishListener.OnFinishGame();
				}
			}
		}
		
		public void PauseGame()
		{
			_state = State.Pause;

			foreach (var gameListener in _listeners)
			{
				if (gameListener is IGamePauseListener gamePauseListener)
				{
					gamePauseListener.OnPause();
				}
			}
		}
		
		public void ResumeGame()
		{
			_state = State.Resume;
			
			foreach (var gameListener in _listeners)
			{
				if (gameListener is IGameResumeListener gameResumeListener)
				{
					gameResumeListener.OnResume();
				}
			}
		}
	}
}

