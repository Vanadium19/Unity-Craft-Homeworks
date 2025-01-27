using System;

namespace Game.Components
{
    public interface IJumper
    {
        public event Action Jumped;

        public void Jump();
    }
}