using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyInstaller _enemyPrefab;

    [SerializeField] private Transform _container;
    [SerializeField] private Transform _worldTransform;
    [SerializeField] private Transform _target;

    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private Transform[] _attackPositions;

    private EnemiesPool _pool;
    private Coroutine _spawning;

    private void Awake()
    {
        _pool = new(_container, _enemyPrefab);
    }

    private void OnEnable()
    {
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

            var enemy = _pool.Pull();

            enemy.transform.position = GetRandomPoint(_spawnPositions);
            enemy.transform.SetParent(_worldTransform);
            
            Vector3 attackPosition = GetRandomPoint(_attackPositions);
            enemy.StartMoving(attackPosition);

            enemy.ResetHealth();
        }
    }  

    private Vector2 GetRandomPoint(Transform[] points)
    {
        int index = Random.Range(0, points.Length);
        return points[index].position;
    }
}