using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContext : MonoBehaviour
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

    private EnemyMoveSource _moveSource;

    public void Initialize()
    {
        gameObject.AddComponent<Health>().Initialize(_health, true);

        _moveSource = gameObject.AddComponent<EnemyMoveSource>();
        gameObject.AddComponent<Mover>().Initialize(_moveSource, _speed);

        IGun gun = gameObject.AddComponent<EnemyGun>().Initialize(_firePoint, _bulletPrefab, _damage, _bulletSpeed);
        IShootEvent shootEvent = gameObject.AddComponent<EnemyShootEvent>().Initialize(_shotDelay);
        gameObject.AddComponent<AttackComponent>().Initialize(gun, shootEvent);
    }

    public void StartMoving(Vector2 destination)
    {
        _moveSource.StartMoving(destination);
    }
}