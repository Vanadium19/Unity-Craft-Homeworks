using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class JumpController : ITickable
    {
        private readonly ICharacter _character;

        public JumpController(ICharacter character)
        {
            _character = character;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _character.Jump();
        }
    }
}