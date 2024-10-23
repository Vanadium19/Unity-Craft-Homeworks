using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyContext _enemyPrefab;

    [SerializeField] private Transform _container;
    [SerializeField] private Transform _worldTransform;
    [SerializeField] private Transform _target;

    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private Transform[] _attackPositions;

    private Queue<EnemyContext> _enemyPool = new();
    private Coroutine _spawning;

    private void Start()
    {
        for (var i = 0; i < 7; i++)
        {
            var enemy = Spawn();
            Push(enemy);
        }

        _spawning = StartCoroutine(StartSpawn());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawning);   
    }

    private IEnumerator StartSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 2));

            var enemy = Pull();

            enemy.transform.position = GetRandomPoint(_spawnPositions);
            
            Vector3 attackPosition = GetRandomPoint(_attackPositions);

            enemy.StartMoving(attackPosition);
        }
    }

    private void Push(EnemyContext enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(_container);
        _enemyPool.Enqueue(enemy);
    }

    private EnemyContext Pull()
    {
        if (_enemyPool.Count == 0)
            return Spawn();

        var enemy = _enemyPool.Dequeue();

        enemy.gameObject.SetActive(true);
        enemy.transform.SetParent(_worldTransform);

        return enemy;
    }

    private EnemyContext Spawn()
    {
        var enemy = Instantiate(_enemyPrefab, _worldTransform);
        enemy.Initialize();
        return enemy;
    }

    private Vector2 GetRandomPoint(Transform[] points)
    {
        int index = Random.Range(0, points.Length);
        return points[index].position;
    }
}