using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstaller : MonoBehaviour
{
    [Header("Characteristics")]
    [SerializeField] private int _health = 5;
    [SerializeField] private float _speed = 5.0f;

    [Header("Weapon")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private int _bulletSpeed = 3;
    [SerializeField] private float _shotDelay = 1f;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;

    private HealthComponent _healthComponent;
    private EnemyMoveSource _moveSource;

    public HealthComponent Health => _healthComponent;

    public void Initialize(Transform target)
    {
        _healthComponent = gameObject.AddComponent<HealthComponent>().Initialize(_health, true);

        _moveSource = gameObject.AddComponent<EnemyMoveSource>();
        gameObject.AddComponent<Mover>().Initialize(_moveSource, _speed);

        IGun gun = gameObject.AddComponent<EnemyGun>().Initialize(_firePoint, target, _bulletPrefab, _damage, _bulletSpeed);
        IShootEvent shootEvent = gameObject.AddComponent<EnemyShootEvent>().Initialize(_shotDelay, _moveSource.CanShoot);
        gameObject.AddComponent<AttackComponent>().Initialize(gun, shootEvent);
    }

    public void StartMoving(Vector2 destination)
    {
        _moveSource.StartMoving(destination);
    }
}