using System;
using UnityEngine;

namespace ShootEmUp
{
	public sealed class HitPointsComponent
	{
		public event Action<GameObject> HpEmpty;
		
		private int _hitPoints;
		private int _maxHitPoints;
		private GameObject _gameObject;
		
		public HitPointsComponent(int hitPoints, GameObject gameObject)
		{
			_hitPoints = hitPoints;
			_maxHitPoints = hitPoints;
			_gameObject = gameObject;
		}
		
		public bool IsHitPointsExists() 
		{
			return _hitPoints > 0;
		}

		public void TakeDamage(int damage)
		{
			_hitPoints -= damage;
			if (_hitPoints <= 0)
				HpEmpty?.Invoke(_gameObject);
		}
		
		public void Reset()
		{
			_hitPoints = _maxHitPoints;
		}
	}
}