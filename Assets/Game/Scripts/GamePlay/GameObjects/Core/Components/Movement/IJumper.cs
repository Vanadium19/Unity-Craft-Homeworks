using System;

namespace Game.Components.Interfaces
{
    public interface IJumper
    {
        public event Action Jumped;

        public void Jump();
    }
}