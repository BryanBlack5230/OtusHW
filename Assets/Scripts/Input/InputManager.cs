using System;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class InputManager : IGameUpdateListener
	{
		public Vector2 HorizontalDirection { get; private set; }
		public Action OnFireEvent;
		private readonly InputConfig _inputConfig;
		
		public InputManager(InputConfig inputConfig) 
		{
			_inputConfig = inputConfig;
			IGameListener.Register(this);
		}

		private void CheckFireKey()
		{
			if (Input.GetKeyDown(_inputConfig.FireKey))
				OnFireEvent?.Invoke();
		}


		private void SetDirection()
		{
			float xPos = 0f;
			if (Input.GetKey(_inputConfig.LeftKey))
			{
				xPos = -1;
			}
			else if (Input.GetKey(_inputConfig.RightKey))
			{
				xPos = 1;
			}
			else
			{
				xPos = 0;
			}

			HorizontalDirection = new(xPos, 0);
		}

		public void OnUpdate(float deltaTime)
		{
			CheckFireKey();
			SetDirection();
		}
	}
}