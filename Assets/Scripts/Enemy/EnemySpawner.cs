using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private BulletManager _bulletManager;
    [SerializeField] private EnemyHealth _enemyPrefab;

    [SerializeField] private Transform _container;
    [SerializeField] private Transform _worldTransform;
    [SerializeField] private Transform _target;

    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private Transform[] _attackPositions;

    private readonly Queue<EnemyHealth> _enemyPool = new();

    private void Start()
    {
        for (var i = 0; i < 7; i++)
        {
            var enemy = Instantiate(_enemyPrefab, _container);
            _enemyPool.Enqueue(enemy);
        }

        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 2));

            if (!_enemyPool.TryDequeue(out EnemyHealth enemy))
                enemy = Instantiate(_enemyPrefab, _container);

            enemy.transform.SetParent(_worldTransform);

            Vector3 spawnPosition = GetRandomPoint(_spawnPositions);
            enemy.transform.position = spawnPosition;

            Vector3 attackPosition = GetRandomPoint(_attackPositions);
            enemy.gameObject.GetComponent<EnemyMover>().Init(attackPosition);
            enemy.gameObject.GetComponent<EnemyAttacker>().Init(_target, _bulletManager);
        }
    }

    private Vector2 GetRandomPoint(Transform[] points)
    {
        int index = Random.Range(0, points.Length);
        return points[index].position;
    }
}