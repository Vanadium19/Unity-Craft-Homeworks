using System;
using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Enemies
{
    public class Trap : MonoBehaviour, IDamagable, IPushable
    {
        private UnityEventReceiver _unityEvents;

        private PushableComponent _pushableComponent;
        private Attacker _attacker;
        private Health _health;
        
        public event Action HealthChanged;

        [Inject]
        public void Construct(UnityEventReceiver unityEvents,
            PushableComponent pushableComponent,
            Attacker attacker,
            Health health)
        {
            _pushableComponent = pushableComponent;
            _unityEvents = unityEvents;
            _attacker = attacker;
            _health = health;
        }

        private void OnEnable()
        {
            _unityEvents.OnCollisionEntered += OnCollisionEntered;
            _attacker.GaveDamage += Die;
            _health.Died += OnDied;
        }

        private void OnDisable()
        {
            _unityEvents.OnCollisionEntered -= OnCollisionEntered;
            _attacker.GaveDamage -= Die;
            _health.Died -= OnDied;
        }

        public void TakeDamage(int damage)
        {
            HealthChanged?.Invoke();

            _health.TakeDamage(damage);
        }

        public void AddForce(Vector2 force)
        {
            _pushableComponent.AddForce(force);
        }

        private void OnCollisionEntered(Collision2D other)
        {
            _attacker.Attack(other.collider);
        }

        private void Die()
        {
            TakeDamage(int.MaxValue);
        }

        private void OnDied()
        {
            gameObject.SetActive(false);
        }
    }
}