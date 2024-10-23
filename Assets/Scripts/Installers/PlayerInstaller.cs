using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [Header("Characteristics")]
    [SerializeField] private int _health = 5;
    [SerializeField] private float _speed = 5.0f;

    [Header("Weapon")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private int _bulletSpeed = 3;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameObject.AddComponent<HealthComponent>().Initialize(_health, false);

        IMoveSource moveSource = gameObject.AddComponent<PlayerMoveSource>();
        gameObject.AddComponent<Mover>().Initialize(moveSource, _speed);

        IGun gun = gameObject.AddComponent<PlayerGun>().Initialize(_firePoint, _bulletPrefab, _damage, _bulletSpeed);
        IShootEvent shootEvent = gameObject.AddComponent<PlayerShootEvent>();
        gameObject.AddComponent<AttackComponent>().Initialize(gun, shootEvent);
    }
}