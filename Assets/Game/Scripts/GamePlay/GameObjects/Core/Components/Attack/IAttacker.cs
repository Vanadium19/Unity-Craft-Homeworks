using System;

namespace Game.Core.Components
{
    public interface IAttacker
    {
        public event Action Attacked;
    }
}