using ShootEmUp.Common;
using System;
using ShootEmUp.Level.Spawners;
using UnityEngine;

namespace ShootEmUp.Ships.Weapons
{
    [Serializable]
    public class Gun
    {
        [SerializeField] private Transform _firePosition;
        [SerializeField] private Color _color;
        [SerializeField] private PhysicsLayer _layer;
        [SerializeField] private float _bulletSpeed = 3f;
        [SerializeField] private int _damage = 1;

        private BulletSpawner _bulletSpawner;
        private bool _isEnenmy;

        public void Initialize(bool isEnemy, BulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
            _isEnenmy = isEnemy;
        }

        public void Shoot(Vector2 direction)
        {
            _bulletSpawner.Spawn(_isEnenmy, _damage, _color, (int)_layer, _firePosition.position, direction * _bulletSpeed);
        }
    }
}