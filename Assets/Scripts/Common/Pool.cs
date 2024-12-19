using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
	public class Pool
	{
		private readonly Queue<GameObject> _pool = new();
		private readonly Func<GameObject> _factoryMethod; 
		private Transform _activeContainer, _inactiveContainer;
		private bool _isFixedAmount;

		public Pool(Func<GameObject> factoryMethod, int initialCount, bool isFixedAmount, Transform activeContainer, Transform inactiveContainer)
		{
			_factoryMethod = factoryMethod;
			_isFixedAmount = isFixedAmount;
			_activeContainer = activeContainer;
			_inactiveContainer = inactiveContainer;
			
			Init(initialCount);
		}
		
		private void Init(int initialCount)
		{
			for (var i = 0; i < initialCount; i++)
			{
				var obj  = _factoryMethod();
				obj.transform.SetParent(_inactiveContainer);
				obj.SetActive(false);
				_pool.Enqueue(obj);
			}
		}
		
		public GameObject Spawn()
		{
			if (_pool.TryDequeue(out GameObject obj))
			{
				obj.transform.SetParent(_activeContainer);
				obj.SetActive(true);
				return obj;
			}

			if (_isFixedAmount) return null;
			
			GameObject newObj = _factoryMethod();
			newObj.transform.SetParent(_activeContainer);
			_pool.Enqueue(newObj);
			return newObj;
		}

		public void Return(GameObject obj)
		{
			obj.transform.SetParent(_inactiveContainer);
			obj.SetActive(false);
			_pool.Enqueue(obj);
		}
	}
}