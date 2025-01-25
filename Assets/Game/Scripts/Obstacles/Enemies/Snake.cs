using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Enemies
{
    public class Snake : MonoBehaviour, IDamagable, IMovable
    {
        private UnityEventReceiver _unityEvents;
        private Transform _transform;

        private TransformMover _mover;
        private TargetPusher _pusher;
        private Attacker _attacker;
        private Rotater _rotater;
        private Health _health;

        public Vector2 Position => _transform.position;

        [Inject]
        public void Construct(UnityEventReceiver unityEvents,
            TargetPusher pusher,
            TransformMover mover,
            Attacker attacker,
            Rotater rotater,
            Health health)
        {
            _unityEvents = unityEvents;
            _attacker = attacker;
            _rotater = rotater;
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
            _health.Died += OnDied;
        }

        private void OnDisable()
        {
            _unityEvents.OnTriggerEntered -= OnTriggerEntered;
            _health.Died -= OnDied;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"Snake take damage " + damage);

            _health.TakeDamage(damage);
        }

        public void Move(Vector2 direction)
        {
            _mover.Move(direction);
            _rotater.Rotate(direction);
        }

        private void OnTriggerEntered(Collider2D other)
        {
            _pusher.Push(other, Vector2.up);
            _attacker.Attack(other);
        }

        private void OnDied()
        {
            gameObject.SetActive(false);
        }
    }
}