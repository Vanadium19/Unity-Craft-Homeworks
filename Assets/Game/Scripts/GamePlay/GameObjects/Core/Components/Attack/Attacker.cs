using System;
using Game.Components.Interfaces;
using UnityEngine;

namespace Game.Components
{
    public class Attacker
    {
        private readonly int _damage;

        public event Action GaveDamage;

        public Attacker(int damage)
        {
            _damage = damage;
        }

        public void Attack(Collider2D collider)
        {
            if (collider.TryGetComponent(out IDamagable target))
            {
                target.TakeDamage(_damage);
                GaveDamage?.Invoke();
            }
        }
    }
}