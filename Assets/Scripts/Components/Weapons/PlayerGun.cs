using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour, IGun
{
    private BulletsPool _pool;
    private Transform _firePoint;
    private float _bulletSpeed;

    public IGun Initialize(Transform firePoint, Bullet prefab, int damage, float bulletSpeed)
    {
        _pool = new(transform, prefab, false, damage, Color.blue, PhysicsLayer.PlayerBullet);
        _firePoint = firePoint;
        _bulletSpeed = bulletSpeed;
        return this;
    }

    public void Shoot()
    {
        var bullet = _pool.Pull();
        bullet.transform.parent = null;
        bullet.Throw(_firePoint.position, Vector2.up * _bulletSpeed);
    }
}