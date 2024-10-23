using System;

namespace ShootEmUp.Components.AttackComponents
{
    public interface IShootEvent
    {
        public event Action Fired;
    }
}