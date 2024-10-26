using System;
using UnityEngine;

namespace ShootEmUp.Ships
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private int _startHealth = 5;

        private bool _isEnemy;
        private int _health;

        public event Action<int> HealthChanged;
        public event Action<Ship> Died;

        public bool IsEnemy => _isEnemy;

        private void Awake()
        {
            ResetHealth();
        }

        public void ResetHealth()
        {
            _health = _startHealth;
        }

        public void TakeDamage(int damage)
        {
            if (_health <= 0)
                return;

            _health = Mathf.Max(0, _health - damage);
            HealthChanged?.Invoke(_health);

            if (_health <= 0)
                Died?.Invoke(this);
        }

        protected void SetIsEnemy(bool isEnemy)
        {
            _isEnemy = isEnemy;
        }
    }
}