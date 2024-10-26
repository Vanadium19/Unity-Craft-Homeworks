using ShootEmUp.Level.Spawners;
using ShootEmUp.Ships;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Pools
{
    public class EnemyPool : Pool<EnemyAI>
    {
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private Transform _player;

        private Dictionary<Ship, EnemyAI> _shipsAI = new();

        protected override void OnPulled(EnemyAI spawnableObject)
        {
            base.OnPulled(spawnableObject);

            _shipsAI.Add(spawnableObject.Ship, spawnableObject);
            spawnableObject.Ship.OnShipDestroyed += OnEnemyDied;
        }

        protected override void OnPushed(EnemyAI spawnableObject)
        {
            base.OnPushed(spawnableObject);

            _shipsAI.Remove(spawnableObject.Ship);
            spawnableObject.Ship.OnShipDestroyed -= OnEnemyDied;
        }

        protected override void OnSpawned(EnemyAI spawnableObject)
        {
            spawnableObject.Initialize(_bulletSpawner, _player);
        }

        private void OnEnemyDied(Ship enemy)
        {
            if (_shipsAI.ContainsKey(enemy))
                Push(_shipsAI[enemy]);
        }
    }
}