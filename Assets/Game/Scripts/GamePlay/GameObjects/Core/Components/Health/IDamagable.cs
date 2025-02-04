using System;

namespace Game.Core.Components
{
    public interface IDamagable
    {
        public event Action<int> HealthChanged;

        public void TakeDamage(int damage);
    }
}