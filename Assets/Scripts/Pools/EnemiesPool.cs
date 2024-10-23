using System.Collections.Generic;
using ShootEmUp.Components.HealthComponents;
using ShootEmUp.Installers;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ShootEmUp.Pools
{
    public class EnemiesPool : Pool<EnemyInstaller>
    {
        private Transform _target;
        private Transform _bulletWorldContainer;

        private Dictionary<Health, EnemyInstaller> _enemies;

        public EnemiesPool(Transform poolContainer,
                           Transform worldContainer,
                           EnemyInstaller prefab,
                           Transform bulletWorldContainer,
                           Transform target) : base(poolContainer, worldContainer, prefab)
        {
            _target = target;
            _bulletWorldContainer = bulletWorldContainer;
            _enemies = new();
        }

        public override EnemyInstaller Pull()
        {
            var enemy = base.Pull();

            enemy.Health.Died += OnEnemyDied;
            return enemy;
        }

        public override void Push(EnemyInstaller spawnableObject)
        {
            spawnableObject.Health.Died -= OnEnemyDied;
            spawnableObject.Health.ResetHealth();

            base.Push(spawnableObject);
        }

        protected override EnemyInstaller Spawn()
        {
            var enemy = base.Spawn();

            enemy.SetTarget(_target);
            enemy.Initialize(true, _bulletWorldContainer);
            _enemies.Add(enemy.Health, enemy);
            return enemy;
        }

        private void OnEnemyDied(Health health)
        {
            if (_enemies.ContainsKey(health))
                Push(_enemies[health]);
        }
    }
}