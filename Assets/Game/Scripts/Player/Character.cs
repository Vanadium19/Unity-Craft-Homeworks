using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Character : MonoBehaviour, ICharacter
    {
        private Mover _mover;

        [Inject]
        public void Construct(Mover mover)
        {
            _mover = mover;
        }

        public void Move(Vector2 direction)
        {
            _mover.Move(direction);
        }
    }
}