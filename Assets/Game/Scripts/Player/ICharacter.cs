using Game.Components;
using UnityEngine;

namespace Game.Player
{
    public interface ICharacter
    {
        public void Move(Vector2 direction);

        public void Jump();

        public void Push(PushDirection direction);
    }
}