using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private BulletManager _bulletManager;
    [SerializeField] private Transform _firePoint;

    [SerializeField] private int _bulletSpeed = 3;
    [SerializeField] private int _damage = 1;

    private Vector2 _bulletVelocity;

    private void Awake()
    {
        _bulletVelocity = Vector2.up * _bulletSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
    }

    private void Attack()
    {
        _bulletManager.SpawnBullet(_firePoint.position,
                                   Color.blue,
                                   (int)PhysicsLayer.PLAYER_BULLET,
                                   _damage,
                                   true,
                                   _firePoint.rotation * _bulletVelocity);
    }
}