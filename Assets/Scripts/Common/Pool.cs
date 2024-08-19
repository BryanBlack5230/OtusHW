using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class Pool
	{
		private readonly Queue<GameObject> _pool = new();
		private GameObject _prefab;
		private Transform _activeContainer, _inactiveContainer;
		private bool _isFixedAmount;

		public Pool(GameObject prefab, int initialCount, bool isFixedAmount, Transform activeContainer, Transform inactiveContainer)
		{
			_prefab = prefab;
			_isFixedAmount = isFixedAmount;
			_activeContainer = activeContainer;
			_inactiveContainer = inactiveContainer;
			
			Init(initialCount);
		}
		
		private void Init(int initialCount)
		{
			for (var i = 0; i < initialCount; i++)
			{
				var bullet = GameObject.Instantiate(_prefab, _inactiveContainer);
				_pool.Enqueue(bullet);
			}
		}
		
		public GameObject Spawn()
		{
			if (_pool.TryDequeue(out GameObject obj))
			{
				obj.transform.SetParent(_activeContainer);
				return obj;
			}

			if (_isFixedAmount) return null;
			
			GameObject newObj = Object.Instantiate(_prefab, _activeContainer);
			_pool.Enqueue(newObj);
			return newObj;
		}

		public void Return(GameObject obj)
		{
			obj.transform.SetParent(_inactiveContainer);
			_pool.Enqueue(obj);
		}
	}
}