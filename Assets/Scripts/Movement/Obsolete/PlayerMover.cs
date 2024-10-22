using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerMoveSource))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private PlayerMoveSource _moveInput;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveInput = GetComponent<PlayerMoveSource>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveStep = _moveInput.Value * Time.fixedDeltaTime * _speed;
        Vector2 targetPosition = _rigidbody.position + moveStep;

        _rigidbody.MovePosition(targetPosition);
    }
}