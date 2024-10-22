using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;

    [SerializeField] private float _delay;
    [SerializeField] private int _damage = 1;
    [SerializeField] private int _bulletSpeed = 2;

    private BulletManager _bulletManager;
    private Transform _target;
    private float _currentTime;

    private void FixedUpdate()
    {
        _currentTime -= Time.fixedDeltaTime;

        if (_currentTime <= 0)
            Attack();
    }

    public void Init(Transform target, BulletManager bulletManager)
    {
        _target = target;
        _bulletManager = bulletManager;
    }

    public void Attack()
    {
        Vector2 direction = (_target.position - _firePoint.position).normalized;

        _bulletManager.SpawnBullet(_firePoint.position,
                                   Color.red,
                                   (int)PhysicsLayer.ENEMY_BULLET,
                                   _damage,
                                   false,
                                   direction * _bulletSpeed);

        _currentTime += _delay;
    }
}