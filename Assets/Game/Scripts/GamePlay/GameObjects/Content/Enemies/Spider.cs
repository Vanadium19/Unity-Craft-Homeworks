using System;
using Game.Core.Components;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Content.Enemies
{
    public class Spider : IInitializable, IDisposable
    {
        private readonly UnityEventReceiver _unityEvents;
        private readonly GameObject _gameObject;

        private readonly TransformMoveComponent _mover;
        private readonly TargetPushComponent _pusher;
        private readonly AttackComponent _attacker;
        private readonly HealthComponent _health;

        public Spider(UnityEventReceiver unityEvents,
            GameObject gameObject,
            TransformMoveComponent mover,
            TargetPushComponent pusher,
            AttackComponent attacker,
            HealthComponent health,
            ForceComponent force)
        {
            _unityEvents = unityEvents;
            _gameObject = gameObject;

            _attacker = attacker;
            _health = health;
            _pusher = pusher;
            _mover = mover;
            
            _mover.AddCondition(() => !force.IsPushing);
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

        private void OnTriggerEntered(Collider2D other)
        {
            Vector2 direction = ((Vector2)other.transform.position - _mover.Position).normalized;

            _pusher.Push(other, direction);
            _attacker.Attack(other);
        }

        private void OnDied()
        {
            _gameObject.SetActive(false);
        }
    }
}