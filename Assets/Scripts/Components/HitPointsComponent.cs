using System;

namespace ShootEmUp
{
	public sealed class HitPointsComponent
	{
		public event Action<HitPointsComponent> HpEmpty;
		
		private int _hitPoints;
		private int _maxHitPoints;
		
		public HitPointsComponent(int hitPoints)
        {
            _hitPoints = hitPoints;
            _maxHitPoints = hitPoints;
        }
		
		public bool IsHitPointsExists() 
		{
			return _hitPoints > 0;
		}

		public void TakeDamage(int damage)
		{
			_hitPoints -= damage;
			if (_hitPoints <= 0)
				HpEmpty?.Invoke(this);
		}
		
		public void Reset()
		{
			_hitPoints = _maxHitPoints;
		}
	}
}