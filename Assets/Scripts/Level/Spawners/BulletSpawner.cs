using ShootEmUp.Pools;
using ShootEmUp.Ships.Weapons;
using UnityEngine;

namespace ShootEmUp.Level.Spawners
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private BulletsPool _bulletsPool;

        public void Spawn(bool isEnemy,
                          int damage,
                          Color color,
                          int layer,
                          Vector3 position,
                          Vector2 velocity)
        {
            Bullet bullet = _bulletsPool.Pull();

            bullet.SetAttackInfo(isEnemy, damage);
            bullet.SetColorAndLayer(color, layer);
            bullet.SetPositionAndVelocity(position, velocity);
        }
    }
}