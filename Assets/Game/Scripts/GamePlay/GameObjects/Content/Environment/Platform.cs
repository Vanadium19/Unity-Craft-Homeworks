using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Environment
{
    public class Platform : MonoBehaviour
    {
        private TransformMoveComponent _mover;
        private Transform _transform;

        [Inject]
        public void Construct(TransformMoveComponent mover)
        {
            _mover = mover;
        }

        private void Awake()
        {
            _transform = transform;
        }
    }
}