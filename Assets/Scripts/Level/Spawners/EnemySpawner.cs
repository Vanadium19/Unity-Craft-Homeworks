using ShootEmUp.Ships;
using System.Collections;
using UnityEngine;

namespace ShootEmUp.Level.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        private readonly WaitForSeconds _delay = new WaitForSeconds(2f);

        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private EnemyShip _enemyPrefab;
        [SerializeField] private Transform _player;

        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;

        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

        private EnemyPool _pool;

        private void Awake()
        {
            _pool = new(_container, _worldTransform, _enemyPrefab, _bulletSpawner, _player);
        }

        private void Start()
        {
            StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            while (true)
            {
                yield return _delay;

                var enemy = _pool.Pull();

                enemy.ResetHealth();
                enemy.transform.position = GetRandomPoint(_spawnPositions);

                Vector3 attackPosition = GetRandomPoint(_attackPositions);
                enemy.SetDestination(attackPosition);
            }
        }

        private Vector2 GetRandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index].position;
        }
    }
}