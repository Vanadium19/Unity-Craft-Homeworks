using UnityEngine;

namespace Game.Components
{
    public class Mover
    {
        private const float RightAngle = 0;
        private const float LeftAngle = 180;

        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly float _speed;

        public Mover(Rigidbody2D rigidbody, Transform transform, float speed)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _speed = speed;
        }

        public void Move(Vector2 direction)
        {
            _rigidbody.velocity = direction * _speed + Vector2.up * _rigidbody.velocity.y;

            if (direction != Vector2.zero)
                Rotate(direction);
        }

        private void Rotate(Vector2 direction)
        {
            float angle = direction.x > 0 ? RightAngle : LeftAngle;

            _transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}