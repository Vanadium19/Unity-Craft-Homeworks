using Game.Components;
using Game.Components.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Environment
{
    public class Platform : MonoBehaviour, IMovable
    {
        private TransformMover _mover;
        private Transform _transform;

        public Vector2 Position => _transform.position;

        [Inject]
        public void Construct(TransformMover mover)
        {
            _mover = mover;
        }

        private void Awake()
        {
            _transform = transform;
        }

        public void Move(Vector2 direction)
        {
            _mover.Move(direction);
        }
    }
}