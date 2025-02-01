using Game.Components.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class PushableComponent : ITickable, IPushable
    {
        private readonly float _delay;
        private readonly Rigidbody2D _rigidbody;

        private bool _isPushing;
        private float _currentTime;

        public PushableComponent(float delay, Rigidbody2D rigidbody)
        {
            _delay = delay;
            _rigidbody = rigidbody;
        }

        public bool IsPushing => _isPushing;

        public void Tick()
        {
            if (!_isPushing)
                return;
            
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
                _isPushing = false;
        }

        public void AddForce(Vector2 force)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(force, ForceMode2D.Impulse);

            if (!Mathf.Approximately(Mathf.Abs(Vector2.Dot(force.normalized, Vector2.up)), 1f))
            {
                _isPushing = true;
                _currentTime = _delay;
            }
        }
    }
}