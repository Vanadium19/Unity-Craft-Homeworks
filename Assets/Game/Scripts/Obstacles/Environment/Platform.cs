using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Environment
{
    public class Platform : MonoBehaviour, IMovable
    {
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

        public void Move(Vector2 direction)
        {
            _mover.Move(direction);
        }
    }
}