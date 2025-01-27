using System;

namespace Game.Components
{
    public interface IAttacker
    {
        public event Action Attacked;
    }
}