using System;

namespace Game.Components
{
    public interface IDamagable
    {
        public event Action HealthChanged;

        public void TakeDamage(int damage);
    }
}