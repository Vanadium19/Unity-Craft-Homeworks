using System;
using UnityEngine;

namespace ShootEmUp.Components.AttackComponents
{
    public class EnemyShootEvent : MonoBehaviour, IShootEvent
    {
        private float _delay;
        private float _currentTime;
        private Func<bool> _canShoot;

        public event Action Fired;

        private void Update()
        {
            if (!_canShoot.Invoke())
                return;

            _currentTime -= Time.deltaTime;

            if (_currentTime > 0)
                return;

            Fired?.Invoke();
            _currentTime = _delay;
        }

        public IShootEvent Initialize(float delay, Func<bool> canShoot)
        {
            _delay = delay;
            _currentTime = delay;
            _canShoot = canShoot;

            return this;
        }
    }
}