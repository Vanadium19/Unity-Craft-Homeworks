using System.Collections;
using UnityEngine;

namespace ShootEmUp.Components.Movement
{
    public class EnemyMoveSource : MonoBehaviour, IMoveSource
    {
        private readonly float _destinationLapping = 0.25f;

        private Vector2 _value;
        private Transform _transform;
        private Coroutine _moving;

        public Vector2 Value => _value;

        private void Awake()
        {
            _transform = transform;
        }

        public void StartMoving(Vector2 destination)
        {
            if (_moving != null)
                StopCoroutine(_moving);

            _moving = StartCoroutine(Moving(destination));
        }

        public bool CanShoot()
        {
            return _value == Vector2.zero;
        }

        private IEnumerator Moving(Vector2 destination)
        {
            float distance = float.MaxValue;

            while (distance > _destinationLapping)
            {
                Vector3 path = destination - (Vector2)_transform.position;
                distance = path.magnitude;
                _value = path.normalized;
                yield return null;
            }

            _value = Vector3.zero;
        }
    }
}