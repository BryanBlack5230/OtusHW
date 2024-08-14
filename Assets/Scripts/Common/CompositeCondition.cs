using System;
using System.Collections.Generic;

namespace ShootEmUp
{
	public class CompositeCondition
	{
		private List<Func<bool>> _conditions = new();

		public CompositeCondition Append(Func<bool> condition) 
		{
			_conditions.Add(condition);
			return this;
		}
		public bool IsTrue()
		{
			foreach (var condition in _conditions)
			{
				if (!condition.Invoke())
					return false;
			}
			return true;
		}
	}
}
