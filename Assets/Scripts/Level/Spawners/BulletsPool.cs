using ShootEmUp.Level.Spawners;
using ShootEmUp.Ships.Weapons;
using UnityEngine;

public class BulletsPool : Pool<Bullet>
{
    public BulletsPool(Transform poolContainer,
                       Transform worldContainer,
                       Bullet prefab) : base(poolContainer, worldContainer, prefab) { }


    protected override void OnPulled(Bullet spawnableObject)
    {
        base.OnPulled(spawnableObject);
        spawnableObject.Collided += OnBulletCollided;
    }

    protected override void OnPushed(Bullet spawnableObject)
    {
        base.OnPushed(spawnableObject);
        spawnableObject.Collided -= OnBulletCollided;
    }

    protected override void OnSpawned(Bullet spawnableObject)
    {
        spawnableObject.Collided += OnBulletCollided;
    }

    private void OnBulletCollided(Bullet bullet)
    {
        Push(bullet);
    }
}