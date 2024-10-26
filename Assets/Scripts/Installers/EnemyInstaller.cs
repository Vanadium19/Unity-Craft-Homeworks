using System;
using ShootEmUp.Common;
using ShootEmUp.Components.AttackComponents;
using ShootEmUp.Components.Movement;
using ShootEmUp.Components.Weapons;
using ShootEmUp.Pools;
using UnityEngine;

namespace ShootEmUp.Installers
{
    public class EnemyInstaller : Installer
    {
        [SerializeField] private float _shotDelay = 1f;

        //private EnemyMoveSource _moveSource;
        private Transform _target;

        public void StartMoving(Vector2 destination)
        {
            //_moveSource.StartMoving(destination);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        protected override void InitializeMover(float speed)
        {
            //_moveSource = gameObject.AddComponent<EnemyMoveSource>();
            //gameObject.AddComponent<Mover>().Initialize(_moveSource, speed);
        }

        protected override void InitializeWeapon(Transform worldBulletContainer, Transform firePoint, Bullet bullet, int damage, int bulletSpeed)
        {
            if (_target == null)
                throw new ArgumentNullException();

            BulletsPool pool = new(transform, worldBulletContainer, bullet, true, damage, Color.red, PhysicsLayer.EnemyBullet);
            IGun gun = new EnemyGun(pool, firePoint, _target, bulletSpeed);

            //IShootEvent shootEvent = gameObject.AddComponent<EnemyShootEvent>().Initialize(_shotDelay, _moveSource.CanShoot);
            //gameObject.AddComponent<AttackComponent>().Initialize(gun, shootEvent);
        }
    }
}