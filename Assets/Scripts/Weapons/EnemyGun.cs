using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour, IGun
{
    private BulletsPool _pool;
    private Transform _firePoint;
    private float _bulletSpeed;

    public IGun Initialize(Transform firePoint, Bullet prefab, int damage, float bulletSpeed)
    {
        _pool = new(transform, prefab, true, damage, Color.red, PhysicsLayer.ENEMY_BULLET);
        _firePoint = firePoint;
        _bulletSpeed = bulletSpeed;
        return this;
    }

    public void Shoot()
    {
        var bullet = _pool.Pull();
        bullet.transform.parent = null;
        bullet.Throw(_firePoint.position, Vector2.down * _bulletSpeed);
    }
}