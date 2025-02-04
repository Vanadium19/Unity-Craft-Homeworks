using UnityEngine;

namespace Game.Core.Components
{
    public class RigidbodyMoveComponent : EntityComponent, IMovable
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;

        public RigidbodyMoveComponent(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public Vector2 Position => _rigidbody.position;

        public void Move(Vector2 direction)
        {
            if (!CheckConditions())
                return;

            Vector2 velocity = direction * _speed + Vector2.up * _rigidbody.velocity.y;

            _rigidbody.velocity = velocity;
        }
    }
}