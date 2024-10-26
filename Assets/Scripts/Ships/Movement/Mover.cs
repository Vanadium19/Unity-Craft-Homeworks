using UnityEngine;

namespace ShootEmUp.Ships.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;

            Vector2 moveStep = direction * Time.fixedDeltaTime * _speed;
            Vector2 targetPosition = _rigidbody.position + moveStep;

            _rigidbody.MovePosition(targetPosition);
        }
    }
}