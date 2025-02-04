using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.GameSytems.Controllers
{
    public class MoveController : IFixedTickable
    {
        private const string HorizontalAxis = "Horizontal";

        private readonly IMovable _movable;
        private readonly IRotatable _rotatable;

        public MoveController(IMovable movable, [InjectOptional] IRotatable rotatable)
        {
            _movable = movable;
            _rotatable = rotatable;
        }

        public void FixedTick()
        {
            Vector2 direction = Input.GetAxis(HorizontalAxis) * Vector2.right;

            _movable.Move(direction);
            _rotatable?.Rotate(direction);
        }
    }
}