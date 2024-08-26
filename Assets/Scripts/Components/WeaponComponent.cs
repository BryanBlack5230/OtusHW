using UnityEngine;

namespace ShootEmUp
{
	public sealed class WeaponComponent
	{
		public Vector2 Position { get; private set; }
		public Quaternion Rotation { get; private set; }

		public WeaponComponent(Vector2 position, Quaternion rotation)
		{
			Position = position;
			Rotation = rotation;
		}
	}
}