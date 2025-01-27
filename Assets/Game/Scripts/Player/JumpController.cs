using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class JumpController : ITickable
    {
        private readonly IJumper _character;

        public JumpController(IJumper character)
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