using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPositions;

        [SerializeField]
        private Transform[] attackPositions;
        
        [SerializeField]
        private Player character;

        [SerializeField]
        private Transform worldTransform;

        [SerializeField]
        private Transform container;

        [SerializeField]
        private Enemy prefab;
        
        [SerializeField]
        private BulletManager _bulletSystem;
        
        private readonly HashSet<Enemy> m_activeEnemies = new();
        private readonly Queue<Enemy> enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < 7; i++)
            {
                Enemy enemy = Instantiate(this.prefab, this.container);
                this.enemyPool.Enqueue(enemy);
            }
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 2));
                
                if (!this.enemyPool.TryDequeue(out Enemy enemy))
                {
                    enemy = Instantiate(this.prefab, this.container);
                }

                enemy.transform.SetParent(this.worldTransform);

                Transform spawnPosition = this.RandomPoint(this.spawnPositions);
                enemy.transform.position = spawnPosition.position;

                Transform attackPosition = this.RandomPoint(this.attackPositions);
                enemy.SetDestination(attackPosition.position);
                enemy.target = this.character;

                if (this.m_activeEnemies.Count < 5 && this.m_activeEnemies.Add(enemy))
                {
                    enemy.OnFire += this.OnFire;
                }
            }
        }

        private void FixedUpdate()
        {
            foreach (Enemy enemy in m_activeEnemies.ToArray())
            {
                if (enemy.health <= 0)
                {
                    enemy.OnFire -= this.OnFire;
                    enemy.transform.SetParent(this.container);

                    m_activeEnemies.Remove(enemy);
                    this.enemyPool.Enqueue(enemy);
                }
            }
        }

        private void OnFire(Vector2 position, Vector2 direction)
        {
            _bulletSystem.SpawnBullet(
                position,
                Color.red,
                (int) PhysicsLayer.ENEMY_BULLET,
                1,
                false,
                direction * 2
            );
        }

        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}