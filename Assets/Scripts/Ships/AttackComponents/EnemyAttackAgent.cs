using ShootEmUp.Common;
using ShootEmUp.Level.Spawners;
using UnityEngine;

namespace ShootEmUp.Ships.AttackComponents
{
    public class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private Transform _firePosition;
        [SerializeField] private float _bulletSpeed = 2f;
        [SerializeField] private float _delay = 1f;
        [SerializeField] private int _damage;

        private BulletSpawner _bulletSpawner;
        private float _currentTime;

        private void Start()
        {
            _currentTime = _delay;
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime > 0)
                return;

            Shoot();
        }

        public void Initialize(BulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
        }

        private void Shoot()
        {
            _bulletSpawner.Spawn(true, _damage, Color.red, (int)PhysicsLayer.EnemyBullet, _firePosition.position, Vector2.down);

            _currentTime = _delay;
        }
    }
}