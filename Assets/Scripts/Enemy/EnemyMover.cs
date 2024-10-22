using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    private readonly float _destinationLapping = 0.25f;

    [SerializeField] private float _speed = 5.0f;

    private Vector2 _destination;
    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private bool _isDestinationReached;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 destination)
    {
        _destination = destination;
        _isDestinationReached = false;
    }

    private void Update()
    {
        if (_isDestinationReached)
            return;

        Move();
    }

    private void Move()
    {
        Vector2 distance = _destination - (Vector2)_transform.position;

        _isDestinationReached = distance.magnitude <= _destinationLapping;

        if (_isDestinationReached)
            return;

        Vector2 moveStep = distance.normalized * Time.fixedDeltaTime * _speed;
        Vector2 nextPosition = _rigidbody.position + moveStep;

        _rigidbody.MovePosition(nextPosition);
    }
}