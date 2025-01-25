using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Environment
{
    public class Lava : MonoBehaviour
    {
        private UnityEventReceiver _unityEvents;
        private Attacker _attacker;

        [Inject]
        public void Construct(UnityEventReceiver unityEvents, Attacker attacker)
        {
            _unityEvents = unityEvents;
            _attacker = attacker;
        }

        private void OnEnable()
        {
            _unityEvents.OnTriggerEntered += OnCollisionEntered;
        }

        private void OnDisable()
        {
            _unityEvents.OnTriggerEntered -= OnCollisionEntered;
        }

        private void OnCollisionEntered(Collider2D other)
        {
            _attacker.Attack(other);
        }
    }
}