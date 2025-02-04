using System;
using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Environment
{
    public class Trampoline : IInitializable, IDisposable, IAttacker
    {
        private readonly UnityEventReceiver _unityEvents;
        private readonly TargetPushComponent _pusher;
        private readonly Transform _transform;

        public event Action Attacked;

        public Trampoline(Transform transform,
            UnityEventReceiver unityEvents,
            TargetPushComponent pusher)
        {
            _transform = transform;
            _unityEvents = unityEvents;
            _pusher = pusher;
        }

        public void Initialize()
        {
            _unityEvents.OnTriggerEntered += OnTriggerEntered;
        }

        public void Dispose()
        {
            _unityEvents.OnTriggerEntered -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider2D target)
        {
            if (_pusher.Push(target, _transform.up))
                Attacked?.Invoke();
        }
    }
}