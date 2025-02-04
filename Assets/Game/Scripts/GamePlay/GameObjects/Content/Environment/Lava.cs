using System;
using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Environment
{
    public class Lava : IInitializable, IDisposable
    {
        private readonly UnityEventReceiver _unityEvents;
        private readonly AttackComponent _attacker;

        public Lava(UnityEventReceiver unityEvents, AttackComponent attacker)
        {
            _unityEvents = unityEvents;
            _attacker = attacker;
        }

        public void Initialize()
        {
            _unityEvents.OnTriggerEntered += OnCollisionEntered;
        }

        public void Dispose()
        {
            _unityEvents.OnTriggerEntered -= OnCollisionEntered;
        }

        private void OnCollisionEntered(Collider2D other)
        {
            _attacker.Attack(other);
        }
    }
}