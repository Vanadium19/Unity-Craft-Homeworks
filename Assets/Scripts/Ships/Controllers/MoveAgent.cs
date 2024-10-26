using UnityEngine;

public class MoveAgent
{
    private readonly float _destinationLapping = 0.25f;
    
    [SerializeField] private Transform _transform;

    private Vector2 _destination;

    public bool DestinationReached => _path.magnitude <= _destinationLapping;
    public Vector2 Direction => _path.normalized;
    private Vector2 _path => _destination - (Vector2)_transform.position;

    public void SetDestination(Vector2 destination)
    {
        _destination = destination;
    }  
}