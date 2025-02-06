using System;
using Game.Core.Components;

namespace Game.Content.Player
{
    public interface ICharacter : IMovable
    {
        public event Action Pushed;
        public event Action Tossed;

        public void Push();
        public void Toss();
    }
}