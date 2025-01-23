using System;
using Game.Components;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Character : MonoBehaviour, ICharacter, IDamagable
    {
        private Transform _transform;

        private Mover _mover;
        private Jumper _jumper;
        private Pusher _pusher;
        private Health _health;

        [Inject]
        public void Construct(Mover mover,
            Jumper jumper,
            Pusher pusher,
            Health health)
        {
            _mover = mover;
            _jumper = jumper;
            _pusher = pusher;
            _health = health;
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _health.Died += OnCharacterDied;
        }

        private void OnDisable()
        {
            _health.Died -= OnCharacterDied;
        }

        public void Move(Vector2 direction)
        {
            _mover.Move(direction);
        }

        public void Jump()
        {
            _jumper.Jump();
        }

        public void Push(PushDirection direction)
        {
            if (direction == PushDirection.Forward)
                _pusher.Push(_transform.right);
            else if (direction == PushDirection.Up)
                _pusher.Push(Vector2.up);
            else
                throw new Exception("Invalid push direction");
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void OnCharacterDied()
        {
            gameObject.SetActive(false);
        }

        #region Debug

        [Button]
        public void KillPlayer()
        {
            TakeDamage(5);
        }

        #endregion
    }
}