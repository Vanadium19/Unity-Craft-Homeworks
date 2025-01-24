using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Enemies
{
    public class Spider : MonoBehaviour, IDamagable, IMovable
    {
        [SerializeField] private UnityEventReceiver _unityEvents;

        private Transform _transform;

        private TargetPusher _pusher;
        private Attacker _attacker;
        private Health _health;
        private Mover _mover;

        public Vector2 Position => _transform.position;

        [Inject]
        public void Construct(TargetPusher pusher,
            Attacker attacker,
            Health health,
            Mover mover)
        {
            _attacker = attacker;
            _health = health;
            _pusher = pusher;
            _mover = mover;
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

        public void TakeDamage(int damage)
        {
            Debug.Log($"Spider take damage " + damage);

            _health.TakeDamage(damage);
        }

        public void Move(Vector2 direction)
        {
            _mover.Move(direction);
        }

        private void OnTriggerEntered(Collider2D other)
        {
            Vector2 direction = (other.transform.position - _transform.position).normalized;

            _pusher.Push(other, direction);
            _attacker.Attack(other);
        }
    }
}