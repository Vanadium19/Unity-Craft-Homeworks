using System;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class MoveController : ITickable
    {
        private const string HorizontalAxis = "Horizontal";

        private readonly ICharacter _character;

        public MoveController(ICharacter character)
        {
            _character = character;
        }

        public void Tick()
        {
            var direction = Input.GetAxis(HorizontalAxis) * Vector2.right;

            _character.Move(direction);
        }
    }
}