using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Enemies
{
    public class Trap : MonoBehaviour, IDamagable
    {
        [SerializeField] private UnityEventReceiver _unityEvents;

        private Attacker _attacker;
        private Health _health;

        [Inject]
        public void Construct(Attacker attacker, Health health)
        {
            _attacker = attacker;
            _health = health;
        }

        private void OnEnable()
        {
            _unityEvents.OnCollisionEntered += OnCollisionEntered;
            _health.Died += OnDied;
        }

        private void OnDisable()
        {
            _unityEvents.OnCollisionEntered -= OnCollisionEntered;
            _health.Died -= OnDied;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"Trap take damage " + damage);

            _health.TakeDamage(damage);
        }

        private void OnCollisionEntered(Collider2D other)
        {
            _attacker.Attack(other);
        }

        private void OnDied()
        {
            gameObject.SetActive(false);
        }
    }
}