using System;
using UnityEngine;

namespace ShootEmUp.Ships
{
    [Serializable]
    public class Health
    {
        [SerializeField] private int _startHealth = 5;

        private int _health;

        public event Action<int> HealthChanged;
        public event Action Died;

        public void TakeDamage(int damage)
        {
            if (_health <= 0)
                return;

            _health = Mathf.Max(0, _health - damage);
            HealthChanged?.Invoke(_health);

            if (_health <= 0)
                Died?.Invoke();
        }

        public void ResetHealth()
        {
            _health = _startHealth;
        }
    }
}