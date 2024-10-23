using ShootEmUp.Common;
using ShootEmUp.Components.AttackComponents;
using ShootEmUp.Components.Movement;
using ShootEmUp.Components.Weapons;
using ShootEmUp.Pools;
using UnityEngine;

namespace ShootEmUp.Installers
{
    public class PlayerInstaller : Installer
    {
        private void Awake()
        {
            Initialize(false);
        }

        protected override void InitializeMover(float speed)
        {
            IMoveSource moveSource = gameObject.AddComponent<PlayerMoveSource>();
            gameObject.AddComponent<Mover>().Initialize(moveSource, speed);
        }

        protected override void InitializeWeapon(Transform firePoint, Bullet bullet, int damage, int bulletSpeed)
        {
            BulletsPool pool = new(transform, bullet, false, damage, Color.blue, PhysicsLayer.PlayerBullet);
            IGun gun = new PlayerGun(pool, firePoint, bulletSpeed);

            IShootEvent shootEvent = gameObject.AddComponent<PlayerShootEvent>();
            gameObject.AddComponent<AttackComponent>().Initialize(gun, shootEvent);
        }
    }
}