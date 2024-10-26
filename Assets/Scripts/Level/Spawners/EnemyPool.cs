using ShootEmUp.Ships;
using UnityEngine;

namespace ShootEmUp.Level.Spawners
{
    public class EnemyPool : Pool<EnemyShip>
    {
        private readonly BulletSpawner _bulletSpawner;
        private readonly Transform _player;

        public EnemyPool(Transform poolContainer,
                         Transform worldContainer,
                         EnemyShip prefab,
                         BulletSpawner bulletSpawner,
                         Transform player) : base(poolContainer, worldContainer, prefab)
        {
            _bulletSpawner = bulletSpawner;
            _player = player;
        }

        protected override void OnPulled(EnemyShip spawnableObject)
        {
            base.OnPulled(spawnableObject);
            spawnableObject.Died += OnEnemyDied;
        }

        protected override void OnPushed(EnemyShip spawnableObject)
        {
            base.OnPushed(spawnableObject);
            spawnableObject.Died -= OnEnemyDied;
        }

        protected override void OnSpawned(EnemyShip spawnableObject)
        {
            spawnableObject.Initialize(_player, _bulletSpawner);
            spawnableObject.Died += OnEnemyDied;
        }

        private void OnEnemyDied(Ship enemy)
        {
            Push((EnemyShip)enemy);
        }
    }
}