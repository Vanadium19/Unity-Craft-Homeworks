using UnityEngine;

namespace Game.Core.Components
{
    public class TransformMoveComponent : EntityComponent
    {
        private readonly Transform _transform;
        private readonly float _speed;

        public TransformMoveComponent(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }

        public void Move(Vector2 direction)
        {
            Vector3 offset = direction * (_speed * Time.deltaTime);

            _transform.position += offset;
        }
    }
}