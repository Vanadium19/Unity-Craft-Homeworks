using UnityEngine;
using Zenject;

namespace Game.Components
{
    public class Jumper : ITickable
    {
        private const float OverlapAngle = 0;

        private readonly Rigidbody2D _rigidbody;
        private readonly float _force;
        private readonly float _delay;

        private readonly Transform _jumpPoint;
        private readonly Vector2 _overlapSize;
        private readonly int _layerMask;

        private float _currentTime;

        public Jumper(Rigidbody2D rigidbody, JumpParams jumpParams)
        {
            _rigidbody = rigidbody;
            _force = jumpParams.Force;
            _delay = jumpParams.Delay;

            _jumpPoint = jumpParams.Point;
            _overlapSize = jumpParams.OverlapSize;
            _layerMask = jumpParams.GroundLayer;
        }

        private bool IsGrounded => Physics2D.OverlapBox(_jumpPoint.position, _overlapSize, OverlapAngle, _layerMask);

        public void Tick()
        {
            if (_currentTime <= 0)
                return;

            _currentTime -= Time.deltaTime;
        }

        public void Jump()
        {
            if (_currentTime > 0)
                return;

            if (!IsGrounded)
                return;

            Vector2 force = Vector2.up * _force;

            _rigidbody.AddForce(force, ForceMode2D.Impulse);
            _currentTime = _delay;
        }
    }
}