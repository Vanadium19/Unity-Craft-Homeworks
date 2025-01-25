using UnityEngine;

namespace Game.Components
{
    public class TransformMover
    {
        private readonly Transform _transform;
        private readonly float _speed;

        public TransformMover(Transform transform, float speed)
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