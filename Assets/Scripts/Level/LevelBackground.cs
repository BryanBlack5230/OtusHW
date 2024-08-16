using System;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class LevelBackground : MonoBehaviour
	{
		private float _startPositionY;
		private float _endPositionY;
		private float _movingSpeedY;
		private float _positionX;
		private float _positionZ;
		private Transform _myTransform;

		[SerializeField] private Params _params;

		private void Awake()
		{
			_startPositionY = _params.startPositionY;
			_endPositionY = _params.endPositionY;
			_movingSpeedY = _params.movingSpeedY;
			_myTransform = transform;
			var position = _myTransform.position;
			_positionX = position.x;
			_positionZ = position.z;
		}

		private void FixedUpdate()
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

		[Serializable]
		public sealed class Params
		{
			[SerializeField] public float startPositionY;
			[SerializeField] public float endPositionY;
			[SerializeField] public float movingSpeedY;
		}
	}
}