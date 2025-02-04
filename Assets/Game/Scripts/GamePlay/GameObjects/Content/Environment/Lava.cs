using System;
using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Environment
{
    public class Lava : MonoBehaviour, IAttacker
    {
        private UnityEventReceiver _unityEvents;
        private AttackComponent _attacker;

        public event Action Attacked;

        [Inject]
        public void Construct(UnityEventReceiver unityEvents, AttackComponent attacker)
        {
            _unityEvents = unityEvents;
            _attacker = attacker;
        }

        private void OnEnable()
        {
            _unityEvents.OnTriggerEntered += OnCollisionEntered;
            _attacker.GaveDamage += OnGaveDamage;
        }

        private void OnDisable()
        {
            _unityEvents.OnTriggerEntered -= OnCollisionEntered;
            _attacker.GaveDamage -= OnGaveDamage;
        }

        private void OnCollisionEntered(Collider2D other)
        {
            _attacker.Attack(other);
        }

        private void OnGaveDamage()
        {
            Attacked?.Invoke();
        }
    }
}