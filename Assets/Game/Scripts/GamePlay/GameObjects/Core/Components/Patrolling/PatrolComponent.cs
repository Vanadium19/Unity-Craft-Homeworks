using UnityEngine;
using Zenject;

namespace Game.Core.Components
{
    public class PatrolComponent : EntityComponent, IFixedTickable
    {
        private readonly Transform _transform;
        private readonly Vector2 _startPosition;
        private readonly Vector2 _endPosition;
        private readonly float _speed;

        private Vector2 _targetPosition;

        public PatrolComponent(Transform transform,
            Vector3 startPosition,
            Vector3 endPosition,
            float speed)
        {
            _transform = transform;
            _startPosition = startPosition;
            _endPosition = endPosition;
            _speed = speed;

            _targetPosition = _startPosition;
        }
        
        public Vector2 Direction => (_targetPosition - (Vector2)_transform.position).normalized;

        public void FixedTick()
        {
            if (!CheckConditions())
                return;
            
            var position = Vector2.MoveTowards(_transform.position, _targetPosition, _speed * Time.fixedDeltaTime);
            _transform.position = position;

            if (_targetPosition == position)
                _targetPosition = _targetPosition == _startPosition ? _endPosition : _startPosition;
        }
    }
}