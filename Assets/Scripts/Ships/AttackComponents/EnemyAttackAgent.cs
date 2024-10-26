using ShootEmUp.Common;
using ShootEmUp.Level.Spawners;
using System;
using UnityEngine;

namespace ShootEmUp.Ships.AttackComponents
{
    public class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private Transform _firePosition;
        [SerializeField] private float _bulletSpeed = 2f;
        [SerializeField] private float _delay = 1f;
        [SerializeField] private int _damage;

        private BulletSpawner _bulletSpawner;
        private Func<bool> _canShoot;
        private Transform _transform;
        private Transform _target;
        private float _currentTime;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _currentTime = _delay;
        }

        private void Update()
        {
            if (!_canShoot.Invoke())
                return;

            _currentTime -= Time.deltaTime;

            if (_currentTime > 0)
                return;

            Shoot();
        }

        public void Initialize(Transform target, BulletSpawner bulletSpawner, Func<bool> canShoot)
        {
            _bulletSpawner = bulletSpawner;
            _canShoot = canShoot;
            _target = target;
        }

        private void Shoot()
        {
            Vector3 velocity = _bulletSpeed * (_target.position - _transform.position).normalized;

            _bulletSpawner.Spawn(true, _damage, Color.red, (int)PhysicsLayer.EnemyBullet, _firePosition.position, velocity);

            _currentTime = _delay;
        }
    }
}