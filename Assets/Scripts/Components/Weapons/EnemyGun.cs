using ShootEmUp.Common;
using ShootEmUp.Pools;
using UnityEngine;

namespace ShootEmUp.Components.Weapons
{
    public class EnemyGun : Gun
    {
        private Transform _firePoint;
        private Transform _target;

        public EnemyGun(BulletsPool pool,
                        Transform firePoint,
                        Transform target,
                        float bulletSpeed) : base(pool, firePoint, bulletSpeed)
        {
            _firePoint = firePoint;
            _target = target;
        }

        protected override Vector2 GetDirection()
        {
            return (_target.position - _firePoint.position).normalized;
        }
    }
}