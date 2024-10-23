using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : Pool<EnemyInstaller>
{
    private Dictionary<Health, EnemyInstaller> _enemies;

    public EnemiesPool(Transform container, EnemyInstaller prefab) : base(container, prefab)
    {
        _enemies = new();
    }

    public override EnemyInstaller Pull()
    {
        var enemy = base.Pull();

        enemy.Health.Died += OnEnemyDied;
        return enemy;
    }

    public override void Push(EnemyInstaller spawnableObject)
    {
        spawnableObject.Health.Died -= OnEnemyDied;

        base.Push(spawnableObject);
    }

    protected override EnemyInstaller Spawn()
    {
        var enemy = base.Spawn();

        enemy.Initialize();
        _enemies.Add(enemy.Health, enemy);
        return enemy;
    }

    private void OnEnemyDied(Health health)
    {
        if (_enemies.ContainsKey(health))
            Push(_enemies[health]);
    }
}