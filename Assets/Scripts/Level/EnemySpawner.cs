using System.Collections;
using ShootEmUp.Installers;
using ShootEmUp.Pools;
using UnityEngine;

namespace ShootEmUp.Level
{
    public class EnemySpawner : MonoBehaviour
    {
        private readonly WaitForSeconds _delay = new WaitForSeconds(2f);

        [SerializeField] private EnemyInstaller _enemyPrefab;

        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;

        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

        private EnemiesPool _pool;
        private Coroutine _spawning;

        public void Initialize(Transform bulletWorldTransform, Transform player)
        {
            _pool = new(_container, _worldTransform, _enemyPrefab, bulletWorldTransform, player);
        }

        public void StartSpawn()
        {
            if (_spawning != null)
                StopCoroutine(_spawning);

            _spawning = StartCoroutine(Spawning());
        }

        private IEnumerator Spawning()
        {
            while (true)
            {
                yield return _delay;

                var enemy = _pool.Pull();

                enemy.transform.position = GetRandomPoint(_spawnPositions);

                Vector3 attackPosition = GetRandomPoint(_attackPositions);
                enemy.StartMoving(attackPosition);
            }
        }

        private Vector2 GetRandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index].position;
        }
    }
}