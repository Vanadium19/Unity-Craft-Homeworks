using System;
using Game.Components;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Character : MonoBehaviour, ICharacter, IDamagable
    {
        private Transform _transform;

        private GroundChecker _groundChecker;
        private Rotater _rotater;
        private Mover _mover;
        private Jumper _jumper;
        private Pusher _pusher;
        private Health _health;

        public Vector2 Position => _transform.position;

        [Inject]
        public void Construct(GroundChecker groundChecker,
            Rotater rotater,
            Mover mover,
            Jumper jumper,
            Pusher pusher,
            Health health)
        {
            _groundChecker = groundChecker;
            _rotater = rotater;
            _mover = mover;
            _jumper = jumper;
            _pusher = pusher;
            _health = health;
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _health.Died += OnCharacterDied;
        }

        private void OnDisable()
        {
            _health.Died -= OnCharacterDied;
        }

        public void Move(Vector2 direction)
        {
            _mover.Move(direction);
            _rotater.Rotate(direction);
        }

        public void Jump()
        {
            if (_groundChecker.IsGrounded)
                _jumper.Jump();
        }

        public void Push(PushDirection direction)
        {
            if (!_groundChecker.IsGrounded)
                return;

            if (direction == PushDirection.Forward)
                _pusher.Push(_transform.right);
            else if (direction == PushDirection.Up)
                _pusher.Push(Vector2.up);
            else
                throw new Exception("Invalid push direction");
        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"Character take damage " + damage);

            _health.TakeDamage(damage);
        }

        private void OnCharacterDied()
        {
            gameObject.SetActive(false);
        }

        #region Debug

        [Button]
        public void KillPlayer()
        {
            TakeDamage(5);
        }

        #endregion
    }
}