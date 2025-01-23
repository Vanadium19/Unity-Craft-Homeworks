using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PushController : ITickable
    {
        private readonly ICharacter _character;

        public PushController(ICharacter character)
        {
            _character = character;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                _character.Push(PushDirection.Forward);
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                _character.Push(PushDirection.Up);
        }
    }
}