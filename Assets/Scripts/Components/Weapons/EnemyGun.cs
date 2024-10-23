using ShootEmUp.Common;
using ShootEmUp.Pools;
using UnityEngine;

namespace ShootEmUp.Components.Weapons
{
    public class EnemyGun : MonoBehaviour, IGun
    {
        private BulletsPool _pool;
        private Transform _firePoint;
        private Transform _target;
        private float _bulletSpeed;

        public IGun Initialize(Transform firePoint,
                               Transform target,
                               Bullet prefab,
                               int damage,
                               float bulletSpeed)
        {
            _pool = new(transform, prefab, true, damage, Color.red, PhysicsLayer.EnemyBullet);
            _firePoint = firePoint;
            _bulletSpeed = bulletSpeed;
            _target = target;
            return this;
        }

        public void Shoot()
        {
            Vector3 direction = (_target.position - _firePoint.position).normalized;
            Bullet bullet = _pool.Pull();

            bullet.Throw(_firePoint.position, direction * _bulletSpeed);
        }
    }
}