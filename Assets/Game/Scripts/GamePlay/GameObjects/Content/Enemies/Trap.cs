using System;
using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Enemies
{
    public class Trap : IInitializable, IDisposable
    {
        private readonly UnityEventReceiver _unityEvents;
        private readonly GameObject _gameObject;

        private readonly AttackComponent _attacker;
        private readonly HealthComponent _health;

        public Trap(UnityEventReceiver unityEvents,
            GameObject gameObject,
            AttackComponent attacker,
            HealthComponent health)
        {
            _unityEvents = unityEvents;
            _gameObject = gameObject;

            _attacker = attacker;
            _health = health;
        }

        public void Initialize()
        {
            _unityEvents.OnCollisionEntered += OnCollisionEntered;
            _attacker.Attacked += Die;
            _health.Died += OnDied;
        }

        public void Dispose()
        {
            _unityEvents.OnCollisionEntered -= OnCollisionEntered;
            _attacker.Attacked -= Die;
            _health.Died -= OnDied;
        }

        private void OnCollisionEntered(Collision2D other)
        {
            _attacker.Attack(other.collider);
        }

        private void Die()
        {
            _health.TakeDamage(int.MaxValue);
        }

        private void OnDied()
        {
            _gameObject.SetActive(false);
        }
    }
}