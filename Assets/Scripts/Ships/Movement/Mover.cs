using UnityEngine;

namespace ShootEmUp.Ships.Movement
{
    public class Mover
    {
        private float _speed;
        private Rigidbody2D _rigidbody;

        public Mover(float speed, Rigidbody2D rigidbody)
        {
            _speed = speed;
            _rigidbody = rigidbody;
        }

        public void Move(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;

            Vector2 moveStep = direction * Time.deltaTime * _speed;
            Vector2 targetPosition = _rigidbody.position + moveStep;

            _rigidbody.MovePosition(targetPosition);
        }
    }
}