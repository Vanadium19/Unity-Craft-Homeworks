using ShootEmUp.Components.Weapons;
using ShootEmUp.Pools;
using UnityEngine;

public abstract class Gun : IGun
{
    private BulletsPool _pool;
    private Transform _firePoint;
    private float _bulletSpeed;

    protected Gun(BulletsPool pool, Transform firePoint, float bulletSpeed)
    {
        _pool = pool;
        _firePoint = firePoint;
        _bulletSpeed = bulletSpeed;
    }

    public void Shoot()
    {
        Bullet bullet = _pool.Pull();
        Vector2 direction = GetDirection();

        bullet.Throw(_firePoint.position, direction * _bulletSpeed);
    }

    protected abstract Vector2 GetDirection();
}