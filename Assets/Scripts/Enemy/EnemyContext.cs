using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContext : MonoBehaviour
{
    [SerializeField] private int _health = 5;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _shotDelay = 1f;

    private void Initialize()
    {
        gameObject.AddComponent<Health>().Initialize(_health);

        IMoveSource moveSource = gameObject.AddComponent<EnemyMoveSource>();
        gameObject.AddComponent<Mover>().Initialize(moveSource, _speed);

        IGun gun = gameObject.AddComponent<EnemyGun>();
        IShootEvent shootEvent = gameObject.AddComponent<EnemyShootEvent>().Initialize(_shotDelay);
        gameObject.AddComponent<AttackComponent>().Initialize(gun, shootEvent);
    }
}