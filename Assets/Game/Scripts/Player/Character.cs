using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Character : MonoBehaviour, ICharacter
    {
        private Mover _mover;
        private Jumper _jumper;

        [Inject]
        public void Construct(Mover mover, Jumper jumper)
        {
            _mover = mover;
            _jumper = jumper;
        }

        public void Move(Vector2 direction)
        {
            _mover.Move(direction);
        }

        public void Jump()
        {
            _jumper.Jump();
        }
    }
}