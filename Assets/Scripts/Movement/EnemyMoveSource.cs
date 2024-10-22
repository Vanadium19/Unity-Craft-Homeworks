using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMoveSource : MonoBehaviour, IMoveSource
{
    private readonly float _destinationLapping = 0.25f;

    private Vector2 _value;
    private Vector2 _destination;
    private Transform _transform;

    public Vector2 Value => _value;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        Vector2 distance = _destination - (Vector2)_transform.position;

        if (distance.magnitude <= _destinationLapping)
            StopMoving();

        _value = distance.normalized;
    }

    public void Init(Vector2 destination)
    {
        _destination = destination;
    }

    private void StopMoving()
    {
        _value = Vector2.zero;
        enabled = false;
    }
}