using Game.Components.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Controllers
{
    public class PointsMoveController : IInitializable, ITickable
    {
        private const float Lapping = 0.05f;
        
        private readonly IMovable _movable;

        private readonly Vector2 _startPosition;
        private readonly Vector2 _endPosition;

        private Vector2 _currentPosition;

        public PointsMoveController(IMovable movable,
            Vector3 startPosition,
            Vector3 endPosition)
        {
            _movable = movable;
            _startPosition = startPosition;
            _endPosition = endPosition;
        }

        public void Initialize()
        {
            _currentPosition = _startPosition;
        }

        public void Tick()
        {
            Move();
        }

        private void Move()
        {
            Vector2 direction = (_currentPosition - _movable.Position).normalized;

            _movable.Move(direction);

            if (Vector2.Distance(_movable.Position, _currentPosition) <= Lapping)
                _currentPosition = _currentPosition == _startPosition ? _endPosition : _startPosition;
        }
    }
}