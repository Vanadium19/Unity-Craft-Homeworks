using System;
using Game.Components;
using Game.Components.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Environment
{
    public class Lava : MonoBehaviour, IAttacker
    {
        private UnityEventReceiver _unityEvents;
        private Attacker _attacker;

        public event Action Attacked;

        [Inject]
        public void Construct(UnityEventReceiver unityEvents, Attacker attacker)
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