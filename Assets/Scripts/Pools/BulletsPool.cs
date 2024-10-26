using ShootEmUp.Ships.Weapons;
using UnityEngine;

namespace ShootEmUp.Pools
{
    public class BulletsPool : Pool<Bullet>
    {
        protected override void OnPulled(Bullet spawnableObject)
        {
            base.OnPulled(spawnableObject);
            spawnableObject.Collided += OnBulletCollided;
        }

        protected override void OnPushed(Bullet spawnableObject)
        {
            base.OnPushed(spawnableObject);
            spawnableObject.Collided -= OnBulletCollided;
        }

        private void OnBulletCollided(Bullet bullet)
        {
            Push(bullet);
        }
    }
}