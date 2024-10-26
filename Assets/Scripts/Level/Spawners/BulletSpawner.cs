using ShootEmUp.Pools;
using ShootEmUp.Ships.Weapons;
using UnityEngine;

namespace ShootEmUp.Level.Spawners
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private Transform _worldContainer;
        [SerializeField] private Bullet _prefab;

        private BulletsPool _bulletsPool;

        private void Awake()
        {
            _bulletsPool = new(_poolContainer, _worldContainer, _prefab);
        }

        public void Spawn(bool isEnemy,
                          int damage,
                          Color color,
                          int layer,
                          Vector3 position,
                          Vector2 velocity)
        {
            var bullet = _bulletsPool.Pull();

            bullet.SetAttackInfo(isEnemy, damage);
            bullet.SetColorAndLayer(color, layer);
            bullet.SetPositionAndVelocity(position, velocity);
        }
    }
}