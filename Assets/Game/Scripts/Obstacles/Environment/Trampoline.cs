using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Environment
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private UnityEventReceiver _unityEvents;

        private TargetPusher _pusher;

        [Inject]
        public void Construct(TargetPusher pusher)
        {
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
            _pusher.Push(target, Vector2.up);
        }
    }
}