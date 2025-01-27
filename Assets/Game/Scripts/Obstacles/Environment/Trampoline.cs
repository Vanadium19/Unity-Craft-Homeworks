using System;
using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Environment
{
    public class Trampoline : MonoBehaviour, IAttacker
    {
        private UnityEventReceiver _unityEvents;
        private TargetPusher _pusher;

        public event Action Attacked;

        [Inject]
        public void Construct(UnityEventReceiver unityEvents, TargetPusher pusher)
        {
            _unityEvents = unityEvents;
            _pusher = pusher;
        }

        private void OnEnable()
        {
            _unityEvents.OnTriggerEntered += OnTriggerEntered;
        }

        private void OnDisable()
        {
            _unityEvents.OnTriggerEntered -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider2D target)
        {
            if (_pusher.Push(target, Vector2.up))
                Attacked?.Invoke();
        }
    }
}