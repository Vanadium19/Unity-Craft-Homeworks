using System;

namespace Game.Core.Components
{
    public interface IDamagable
    {
        public event Action HealthChanged;

        public void TakeDamage(int damage);
    }
}