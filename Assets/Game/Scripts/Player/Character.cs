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

        private PushableComponent _pushableComponent;
        private GroundChecker _groundChecker;
        private Rotater _rotater;
        private Mover _mover;
        private Jumper _jumper;
        private Pusher _pusher;
        private Health _health;

        private Transform _currentParent;

        public Vector2 Position => _transform.position;

        [Inject]
        public void Construct(GroundChecker groundChecker,
            PushableComponent pushableComponent,
            Rotater rotater,
            Mover mover,
            Jumper jumper,
            Pusher pusher,
            Health health)
        {
            _pushableComponent = pushableComponent;
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
            _groundChecker.ParentChanged += SetParent;
        }

        private void OnDisable()
        {
            _health.Died -= OnCharacterDied;
            _groundChecker.ParentChanged -= SetParent;
        }

        public void Move(Vector2 direction)
        {
            if (_pushableComponent.IsPushing)
                return;
            
            _transform.SetParent(null);

            _mover.Move(direction);
            _rotater.Rotate(direction);
            
            _transform.SetParent(_currentParent);
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

        public void AddForce(Vector2 force)
        {
            _pushableComponent.AddForce(force);
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

        private void SetParent(Transform parent)
        {
            _transform.SetParent(parent);
            _currentParent = parent;
        }
    }
}