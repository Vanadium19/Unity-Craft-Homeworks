using UnityEngine;
using Zenject;

namespace Game.Core.Components
{
    public class PushComponent : EntityComponent, ITickable
    {
        private const int ColliderBufferSize = 5;

        private readonly float _delay;
        private readonly float _force;
        private readonly float _radius;

        private readonly int _layerMask;
        private readonly Transform _pushPoint;

        private float _currentTime;

        public PushComponent(PushParams pushParams)
        {
            _delay = pushParams.Delay;
            _force = pushParams.Force;
            _radius = pushParams.Radius;

            _pushPoint = pushParams.Point;
            _layerMask = pushParams.Mask;
        }

        public void Tick()
        {
            if (_currentTime <= 0)
                return;

            _currentTime -= Time.deltaTime;
        }

        public bool Push(Vector2 direction)
        {
            if (_currentTime > 0)
                return false;

            System.Buffers.ArrayPool<Collider2D> arrayPool = System.Buffers.ArrayPool<Collider2D>.Shared;
            Collider2D[] colliders = arrayPool.Rent(ColliderBufferSize);

            int size = Physics2D.OverlapCircleNonAlloc(_pushPoint.position, _radius, colliders, _layerMask);

            for (int i = 0; i < size; i++)
            {
                if (colliders[i].TryGetComponent(out IPushable pushable))
                {
                    pushable.AddForce(direction * _force);
                }
            }

            if (size > 0)
                _currentTime = _delay;

            arrayPool.Return(colliders);
            return true;
        }
    }
}