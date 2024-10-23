using ShootEmUp.Pools;
using UnityEngine;

namespace ShootEmUp.Components.Weapons
{
    public class PlayerGun : Gun
    {
        public PlayerGun(BulletsPool pool,
                         Transform firePoint,
                         float bulletSpeed) : base(pool, firePoint, bulletSpeed) { }

        protected override Vector2 GetDirection()
        {
            return Vector2.up;
        }
    }
}