using UnityEngine;
using Zenject;

namespace Game.Core.Components
{
    public class Jumper : ITickable
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _force;
        private readonly float _delay;

        private float _currentTime;

        public Jumper(Rigidbody2D rigidbody, float force, float delay)
        {
            _rigidbody = rigidbody;
            _force = force;
            _delay = delay;
        }

        public void Tick()
        {
            if (_currentTime <= 0)
                return;

            _currentTime -= Time.deltaTime;
        }

        public bool Jump()
        {
            if (_currentTime > 0)
                return false;

            Vector2 force = Vector2.up * _force;

            _rigidbody.AddForce(force, ForceMode2D.Impulse);
            _currentTime = _delay;
            return true;
        }
    }
}