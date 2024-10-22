using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContext : MonoBehaviour
{
    [SerializeField] private int _health = 5;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _shotDelay = 1f;

    private EnemyMoveSource _moveSource;

    public void Initialize()
    {
        gameObject.AddComponent<Health>().Initialize(_health);

        _moveSource = gameObject.AddComponent<EnemyMoveSource>();
        gameObject.AddComponent<Mover>().Initialize(_moveSource, _speed);

        IGun gun = gameObject.AddComponent<EnemyGun>();
        IShootEvent shootEvent = gameObject.AddComponent<EnemyShootEvent>().Initialize(_shotDelay);
        gameObject.AddComponent<AttackComponent>().Initialize(gun, shootEvent);
    }

    public void StartMoving(Vector2 destination)
    {
        _moveSource.StartMoving(destination);
    }
}