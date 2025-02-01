using System;
using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Environment
{
    public class Trampoline : MonoBehaviour, IAttacker
    {
        private UnityEventReceiver _unityEvents;
        private TargetPusher _pusher;
        private Transform _transform;

        public event Action Attacked;

        [Inject]
        public void Construct(UnityEventReceiver unityEvents, TargetPusher pusher)
        {
            _unityEvents = unityEvents;
            _pusher = pusher;
        }

        private void Awake()
        {
            _transform = transform;
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
            if (_pusher.Push(target, _transform.up))
                Attacked?.Invoke();
        }
    }
}