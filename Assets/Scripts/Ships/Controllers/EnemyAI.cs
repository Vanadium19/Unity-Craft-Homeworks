using ShootEmUp.Level.Spawners;
using ShootEmUp.Ships.Controllers.Agents;
using UnityEngine;

namespace ShootEmUp.Ships.Controllers
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private AttackAgent _attackAgent;
        [SerializeField] private MoveAgent _moveAgent;
        [SerializeField] private Ship _ship;

        private Transform _transform;
        private Transform _target;
        private bool _isMove;

        public Ship Ship => _ship;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _attackAgent.Fired += Shoot;
        }

        private void Update()
        {
            if (_isMove)
            {
                _ship.Move(_moveAgent.Direction);
                _isMove = !_moveAgent.DestinationReached;
            }

            if (!_isMove)
                _attackAgent.Update();
        }

        private void OnDisable()
        {
            _attackAgent.Fired -= Shoot;
        }

        public void Initialize(BulletSpawner bulletSpawner, Transform target)
        {
            _target = target;
            _ship.Initialize(bulletSpawner);
        }

        public void StartMove(Vector2 destination)
        {
            _isMove = true;
            _moveAgent.SetDestination(destination);
        }

        private void Shoot()
        {
            Vector2 direction = (_target.position - _transform.position).normalized;

            _ship.Shoot(direction);
        }
    }
}