using System;
using UnityEngine;

namespace ShootEmUp.Ships.Movement
{
    [Serializable]
    public class MoveComponent
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;

        public void Move(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;

            Vector2 moveStep = direction * Time.deltaTime * _speed;
            Vector2 targetPosition = _rigidbody.position + moveStep;

            _rigidbody.MovePosition(targetPosition);
        }
    }
}