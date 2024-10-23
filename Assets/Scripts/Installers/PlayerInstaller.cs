using ShootEmUp.Components.AttackComponents;
using ShootEmUp.Components.Movement;
using ShootEmUp.Components.Weapons;
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
            IGun gun = gameObject.AddComponent<PlayerGun>().Initialize(firePoint, bullet, damage, bulletSpeed);
            IShootEvent shootEvent = gameObject.AddComponent<PlayerShootEvent>();
            gameObject.AddComponent<AttackComponent>().Initialize(gun, shootEvent);
        }
    }
}