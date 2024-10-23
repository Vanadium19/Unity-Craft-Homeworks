using ShootEmUp.Components.HealthComponents;
using ShootEmUp.Components.Weapons;
using UnityEngine;

public abstract class Installer : MonoBehaviour
{
    [Header("Characteristics")]
    [SerializeField] private int _health = 5;
    [SerializeField] private float _speed = 5.0f;

    [Header("Weapon")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private int _bulletSpeed = 3;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;

    private Health _healthComponent;

    public Health Health => _healthComponent;

    public void Initialize(bool isEnemy)
    {
        _healthComponent = gameObject.AddComponent<Health>().Initialize(_health, isEnemy);
        InitializeMover(_speed);
        InitializeWeapon(_firePoint, _bulletPrefab, _damage, _bulletSpeed);
    }

    protected abstract void InitializeMover(float speed);

    protected abstract void InitializeWeapon(Transform firePoint, Bullet bullet, int damage, int bulletSpeed);
}