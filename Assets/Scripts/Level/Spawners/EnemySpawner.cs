using System.Collections;
using ShootEmUp.Pools;
using ShootEmUp.Ships;
using UnityEngine;

namespace ShootEmUp.Level.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        private readonly WaitForSeconds _delay = new(2f);

        [SerializeField] private EnemyPool _pool;
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

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

                enemy.transform.position = GetRandomPoint(_spawnPositions);

                Vector3 attackPosition = GetRandomPoint(_attackPositions);
                enemy.StartMove(attackPosition);
            }
        }

        private Vector2 GetRandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index].position;
        }
    }
}