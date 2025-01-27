using System;

namespace Game.Components
{
    public interface IPusher
    {
        public event Action Pushed;
        public event Action Tossed;

        public void Push();
        public void Toss();
    }
}