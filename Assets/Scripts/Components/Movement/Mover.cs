using UnityEngine;

namespace ShootEmUp.Components.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        private IMoveSource _moveSource;
        private Rigidbody2D _rigidbody;
        private float _speed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void Initialize(IMoveSource moveSource, float speed)
        {
            _moveSource = moveSource;
            _speed = speed;
        }

        private void Move()
        {
            if (_moveSource.Value == Vector2.zero)
                return;

            Vector2 moveStep = _moveSource.Value * Time.fixedDeltaTime * _speed;
            Vector2 targetPosition = _rigidbody.position + moveStep;

            _rigidbody.MovePosition(targetPosition);
        }
    }
}