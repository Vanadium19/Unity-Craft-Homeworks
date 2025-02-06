using System;
using Game.Core.Components;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Content.Player
{
    public class Character : IInitializable, IDisposable, ICharacter
    {
        private readonly HealthComponent _health;
        private readonly Transform _transform;

        private readonly GroundChecker _groundChecker;
        private readonly MoveComponent _mover;
        private readonly PlayerPushComponent _pusher;

        private Transform _currentParent;

        public event Action Pushed;
        public event Action Tossed;

        public Character(HealthComponent health,
            Transform transform,
            GroundChecker groundChecker,
            MoveComponent mover,
            PlayerPushComponent pusher,
            ForceComponent forcer,
            JumpComponent jumper,
            RotateComponent rotater)
        {
            _health = health;
            _transform = transform;

            _groundChecker = groundChecker;
            _mover = mover;

            _pusher = pusher;

            SetConditions(groundChecker, forcer, jumper, rotater, mover);
        }

        public void Initialize()
        {
            _health.Died += OnCharacterDied;
            _groundChecker.ParentChanged += SetParent;
        }

        public void Dispose()
        {
            _health.Died -= OnCharacterDied;
            _groundChecker.ParentChanged -= SetParent;
        }

        public void Move(Vector2 direction)
        {
            _transform.SetParent(null);
            _mover.Move(direction);
            _transform.SetParent(_currentParent);
        }

        public void Push()
        {
            if (_pusher.Push(_transform.right))
                Pushed?.Invoke();
        }

        public void Toss()
        {
            if (!_groundChecker.IsGrounded())
                return;

            if (_pusher.Push(Vector2.up))
                Tossed?.Invoke();
        }

        private void SetConditions(GroundChecker groundChecker,
            ForceComponent forcer,
            JumpComponent jumper,
            RotateComponent rotater,
            MoveComponent mover)
        {
            jumper.AddCondition(groundChecker.IsGrounded);
            mover.AddCondition(() => !forcer.IsPushing);
            rotater.AddCondition(() => !forcer.IsPushing);
        }

        private void OnCharacterDied()
        {
            _transform.gameObject.SetActive(false);
        }

        private void SetParent(Transform parent)
        {
            _transform.SetParent(parent);
            _currentParent = parent;
        }
    }
}