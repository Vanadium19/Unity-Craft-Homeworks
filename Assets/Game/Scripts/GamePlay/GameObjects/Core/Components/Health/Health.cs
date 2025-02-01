using System;
using UnityEngine;

namespace Game.Components
{
    public class Health
    {
        private readonly int _health;

        private int _currentHealth;

        public event Action<int> HealthChanged;
        public event Action Died;

        public Health(int health)
        {
            _health = health;
            _currentHealth = health;
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                return;

            if (_currentHealth <= 0)
                return;

            _currentHealth = Mathf.Max(0, _currentHealth - damage);

            HealthChanged?.Invoke(_currentHealth);

            if (_currentHealth <= 0)
                Died?.Invoke();
        }
    }
}