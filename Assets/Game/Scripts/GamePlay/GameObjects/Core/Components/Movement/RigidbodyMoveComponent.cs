using UnityEngine;

namespace Game.Core.Components
{
    public class RigidbodyMoveComponent : EntityComponent
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;

        public RigidbodyMoveComponent(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void Move(Vector2 direction)
        {
            Vector2 velocity = direction * _speed + Vector2.up * _rigidbody.velocity.y;

            _rigidbody.velocity = velocity;
        }
    }
}