using System;

namespace Game.Components.Interfaces
{
    public interface IAttacker
    {
        public event Action Attacked;
    }
}