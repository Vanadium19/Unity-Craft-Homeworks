using System;

namespace Game.Components.Interfaces
{
    public interface IPusher
    {
        public event Action Pushed;
        public event Action Tossed;

        public void Push();
        public void Toss();
    }
}