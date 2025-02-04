using System;
using UnityEngine;

namespace Game.Core.Components
{
    public class AttackComponent : EntityComponent
    {
        private readonly int _damage;

        public event Action GaveDamage;

        public AttackComponent(int damage)
        {
            _damage = damage;
        }

        public void Attack(Collider2D collider)
        {
            if (collider.TryGetComponent(out IEntity entity))
            {
                if (entity.TryGet(out IDamagable target))
                {
                    target.TakeDamage(_damage);
                    GaveDamage?.Invoke();
                }
            }
        }
    }
}