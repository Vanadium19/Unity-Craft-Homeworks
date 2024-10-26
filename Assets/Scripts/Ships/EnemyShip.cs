using ShootEmUp.Level.Spawners;
using ShootEmUp.Ships.AttackComponents;
using ShootEmUp.Ships.Movement;
using UnityEngine;

namespace ShootEmUp.Ships
{
    public class EnemyShip : Ship
    {
        [SerializeField] private EnemyMoveAgent _moveAgent;
        [SerializeField] private EnemyAttackAgent _attackAgent;

        public void Initialize(Transform target, BulletSpawner bulletSpawner)
        {
            SetIsEnemy(true);
            _attackAgent.Initialize(target, bulletSpawner, _moveAgent.CanShoot);
        }

        public void SetDestination(Vector3 destination)
        {
            _moveAgent.StartMoving(destination);
        }
    }
}