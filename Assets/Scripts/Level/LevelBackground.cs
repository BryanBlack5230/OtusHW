using System;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class LevelBackground : MonoBehaviour, IGameFixedUpdateListener
	{
		private float _startPositionY;
		private float _endPositionY;
		private float _movingSpeedY;
		private float _positionX;
		private float _positionZ;
		private Transform _myTransform;

		[SerializeField] private BackgroundSettings _backgroundSettings;

		public void OnFixedUpdate(float fixedDeltaTime)
		{
			if (_myTransform.position.y <= _endPositionY)
			{
				_myTransform.position = new Vector3(
					_positionX,
					_startPositionY,
					_positionZ
				);
			}

			_myTransform.position -= new Vector3(
				_positionX,
				_movingSpeedY * Time.fixedDeltaTime,
				_positionZ
			);
		}

		private void Awake()
		{
			_startPositionY = _backgroundSettings.startPositionY;
			_endPositionY = _backgroundSettings.endPositionY;
			_movingSpeedY = _backgroundSettings.movingSpeedY;
			_myTransform = transform;
			var position = _myTransform.position;
			_positionX = position.x;
			_positionZ = position.z;
		}

		[Serializable]
		public sealed class BackgroundSettings
		{
			[SerializeField] public float startPositionY;
			[SerializeField] public float endPositionY;
			[SerializeField] public float movingSpeedY;
		}
	}
}