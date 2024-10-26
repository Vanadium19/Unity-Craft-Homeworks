using ShootEmUp.Common;
using ShootEmUp.Level.Spawners;
using UnityEngine;

namespace ShootEmUp.Ships.AttackComponents
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private Transform _firePosition;
        [SerializeField] private float _bulletSpeed = 3f;
        [SerializeField] private int _damage = 1;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }

        private void Shoot()
        {
            var velocity = _bulletSpeed * Vector2.up;

            _bulletSpawner.Spawn(false, _damage, Color.blue, (int)PhysicsLayer.PlayerBullet, _firePosition.position, velocity);
        }
    }
}