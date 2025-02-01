using System;

namespace Game.Core.Components
{
    public interface IJumper
    {
        public event Action Jumped;

        public void Jump();
    }
}