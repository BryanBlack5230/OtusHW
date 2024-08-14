using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position {get { return this._firePoint.position; }}
        public Quaternion Rotation {get { return this._firePoint.rotation; }}
        [SerializeField] private Transform _firePoint;
    }
}