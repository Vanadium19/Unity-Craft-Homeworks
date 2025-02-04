using System;
using Game.Core.Components;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Content.Enemies
{
    public class Spider : MonoBehaviour
    {
        private UnityEventReceiver _unityEvents;
        private Transform _transform;

        private ForceComponent _pushableComponent;
        private TransformMoveComponent _mover;
        private TargetPushComponent _pusher;
        private AttackComponent _attacker;
        private HealthComponent _health;

        [Inject]
        public void Construct(UnityEventReceiver unityEvents,
            ForceComponent pushableComponent,
            TargetPushComponent pusher,
            TransformMoveComponent mover,
            AttackComponent attacker,
            HealthComponent health)
        {
            _pushableComponent = pushableComponent;
            _unityEvents = unityEvents;
            _attacker = attacker;
            _health = health;
            _pusher = pusher;
            _mover = mover;
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _unityEvents.OnTriggerEntered += OnTriggerEntered;
            _health.Died += OnDied;
        }

        private void OnDisable()
        {
            _unityEvents.OnTriggerEntered -= OnTriggerEntered;
            _health.Died -= OnDied;
        }

        private void OnTriggerEntered(Collider2D other)
        {
            Vector2 direction = (other.transform.position - _transform.position).normalized;

            _pusher.Push(other, direction);
            _attacker.Attack(other);
        }

        private void OnDied()
        {
            gameObject.SetActive(false);
        }
    }
}