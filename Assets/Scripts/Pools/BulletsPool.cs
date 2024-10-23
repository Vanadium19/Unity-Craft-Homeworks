using ShootEmUp;
using ShootEmUp.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : Pool<Bullet>
{
    private bool _isEnemy;
    private int _damage;
    private Color _color;
    private int _layer;

    public BulletsPool(Transform container,
                       Bullet prefab,
                       bool isEnemy,
                       int damage,
                       Color color,
                       PhysicsLayer layer) : base(container, prefab)
    {
        _isEnemy = isEnemy;
        _damage = damage;
        _color = color;
        _layer = (int)layer;
    }

    public override Bullet Pull()
    {
        var bullet = base.Pull();

        bullet.Collided += Push;
        return bullet;
    }

    public override void Push(Bullet spawnableObject)
    {
        spawnableObject.Collided -= Push;

        base.Push(spawnableObject);
    }

    protected override Bullet Spawn()
    {
        var bullet = base.Spawn();

        bullet.Initialize(_isEnemy, _damage, _color, _layer);
        return bullet;
    }
}