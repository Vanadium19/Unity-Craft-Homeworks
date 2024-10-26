using System.Collections;
using UnityEngine;

namespace ShootEmUp.Ships.Movement
{
    [RequireComponent(typeof(Mover))]
    public class EnemyMoveAgent : MonoBehaviour
    {
        private readonly float _destinationLapping = 0.25f;

        private Mover _mover;
        private Coroutine _moving;
        private Transform _transform;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _transform = transform;
        }

        public void StartMoving(Vector2 destination)
        {
            if (_moving != null)
                StopCoroutine(_moving);

            _moving = StartCoroutine(Moving(destination));
        }

        private IEnumerator Moving(Vector2 destination)
        {
            float distance = float.MaxValue;

            while (distance > _destinationLapping)
            {
                Vector3 path = destination - (Vector2)_transform.position;
                distance = path.magnitude;

                _mover.Move(path.normalized);

                yield return null;
            }
        }
    }
}