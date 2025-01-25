using Game.Components;
using UnityEngine;

namespace Game.Player
{
    public interface ICharacter : IMovable, IPushable
    {
        public void Jump();

        public void Push(PushDirection direction);
    }
}