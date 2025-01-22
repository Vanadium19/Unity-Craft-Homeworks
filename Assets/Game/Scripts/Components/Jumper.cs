using UnityEngine;

namespace Game.Components
{
    public class Jumper
    {
        private readonly float _force;
        private readonly Rigidbody2D _rigidbody;

        public Jumper(float force, Rigidbody2D rigidbody)
        {
            _force = force;
            _rigidbody = rigidbody;
        }

        public void Jump()
        {
            Vector2 force = Vector2.up * _force;

            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}