using System;
using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Environment
{
    public class Platform : MonoBehaviour, IMovable
    {
        [SerializeField] private UnityEventReceiver _unityEvents;

        private Transform _transform;
        private Mover _mover;

        public Vector2 Position => _transform.position;

        [Inject]
        public void Construct(Mover mover)
        {
            _mover = mover;
        }

        private void Awake()
        {
            _transform = transform;
        }

        // private void OnEnable()
        // {
        //     _unityEvents.OnCollisionEntered += OnCollisionEntered;
        //     _unityEvents.OnCollisionExited += OnCollisionExited;
        // }
        //
        // private void OnDisable()
        // {
        //     _unityEvents.OnCollisionEntered -= OnCollisionEntered;
        //     _unityEvents.OnCollisionExited -= OnCollisionExited;
        // }

        public void Move(Vector2 direction)
        {
            _mover.Move(direction);
        }

        // private void OnCollisionEntered(Collider2D target)
        // {
        //     if (target.transform.name == "Character")
        //     {
        //         Debug.Log("Привязал!");
        //         target.transform.SetParent(_transform);
        //     }
        // }
        //
        // private void OnCollisionExited(Collider2D target)
        // {
        //     if (target.transform.name == "Character")
        //     {
        //         Debug.Log("Отвязал!");
        //         target.transform.SetParent(null);
        //     }
        // }
    }
}