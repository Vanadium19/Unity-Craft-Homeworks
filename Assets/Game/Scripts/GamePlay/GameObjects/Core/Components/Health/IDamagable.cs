using System;

namespace Game.Components.Interfaces
{
    public interface IDamagable
    {
        public event Action HealthChanged;

        public void TakeDamage(int damage);
    }
}