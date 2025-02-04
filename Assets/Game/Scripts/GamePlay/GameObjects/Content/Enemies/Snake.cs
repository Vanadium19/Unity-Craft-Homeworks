using System;
using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Enemies
{
    public class Snake : IInitializable, IDisposable
    {
        private readonly UnityEventReceiver _unityEvents;
        private readonly GameObject _gameObject;

        private readonly TargetPushComponent _pusher;
        private readonly AttackComponent _attacker;
        private readonly HealthComponent _health;

        public Snake(UnityEventReceiver unityEvents,
            GameObject gameObject,
            TargetPushComponent pusher,
            AttackComponent attacker,
            HealthComponent health,
            TransformMoveComponent mover,
            ForceComponent force)
        {
            _unityEvents = unityEvents;
            _gameObject = gameObject;
            _attacker = attacker;
            _health = health;
            _pusher = pusher;

            SetConditions(mover, force);
        }

        public void Initialize()
        {
            _unityEvents.OnTriggerEntered += OnTriggerEntered;
            _health.Died += OnDied;
        }

        public void Dispose()
        {
            _unityEvents.OnTriggerEntered -= OnTriggerEntered;
            _health.Died -= OnDied;
        }

        private void SetConditions(TransformMoveComponent mover, ForceComponent force)
        {
            mover.AddCondition(() => !force.IsPushing);
        }

        private void OnTriggerEntered(Collider2D other)
        {
            _pusher.Push(other, Vector2.up);
            _attacker.Attack(other);
        }

        private void OnDied()
        {
            _gameObject.SetActive(false);
        }
    }
}