using System;
using ShootEmUp.Level.Spawners;
using ShootEmUp.Ships.Movement;
using ShootEmUp.Ships.Weapons;
using UnityEngine;

namespace ShootEmUp.Ships
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private bool _isEnemy;
        [SerializeField] private Health _health;
        [SerializeField] private MoveComponent _mover;
        [SerializeField] private Gun _gun;

        public event Action<Ship> OnShipDestroyed;

        public bool IsEnemy => _isEnemy;

        private void OnEnable()
        {
            _health.Died += OnDied;
        }

        private void OnDisable()
        {
            _health.Died -= OnDied;
        }

        public void Initialize(BulletSpawner bulletSpawner)
        {
            _gun.Initialize(_isEnemy, bulletSpawner);
            _health.ResetHealth();
        }

        public void Move(Vector2 direction)
        {
            _mover.Move(direction);
        }

        public void Shoot(Vector2 direction)
        {
            _gun.Shoot(direction);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void OnDied()
        {
            _health.ResetHealth();
            OnShipDestroyed?.Invoke(this);
        }
    }
}