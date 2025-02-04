using System;
using UnityEngine;

namespace Game.Core.Components
{
    public class AttackComponent : EntityComponent, IAttacker
    {
        private readonly int _damage;

        public event Action Attacked;

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
                    Attacked?.Invoke();
                }
            }
        }
    }
}