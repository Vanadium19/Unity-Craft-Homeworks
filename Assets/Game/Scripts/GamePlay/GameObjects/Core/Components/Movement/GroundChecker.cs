using System;
using UnityEngine;
using Zenject;

namespace Game.Core.Components
{
    public class GroundChecker : IInitializable, IDisposable
    {
        private const string Platform = "Platform";
        private const float OverlapAngle = 0;

        private readonly UnityEventReceiver _unityEvents;

        private readonly Transform _jumpPoint;
        private readonly Vector2 _overlapSize;
        private readonly int _layerMask;

        public event Action<Transform> ParentChanged;

        public GroundChecker(UnityEventReceiver unityEvents, GroundCheckParams checkParams)
        {
            _unityEvents = unityEvents;

            _jumpPoint = checkParams.Point;
            _overlapSize = checkParams.OverlapSize;
            _layerMask = checkParams.GroundLayer;
        }

        public void Initialize()
        {
            _unityEvents.OnCollisionEntered += OnCollisionEntered;
            _unityEvents.OnCollisionExited += OnCollisionExited;
        }

        public void Dispose()
        {
            _unityEvents.OnCollisionEntered -= OnCollisionEntered;
            _unityEvents.OnCollisionExited -= OnCollisionExited;
        }

        public bool IsGrounded()
        {
            return Physics2D.OverlapBox(_jumpPoint.position, _overlapSize, OverlapAngle, _layerMask);
        }

        private void OnCollisionEntered(Collision2D target)
        {
            if (target.gameObject.CompareTag(Platform) && CheckNormal(target))
                ParentChanged?.Invoke(target.collider.transform);
        }

        private void OnCollisionExited(Collision2D target)
        {
            if (target.gameObject.CompareTag(Platform))
                ParentChanged?.Invoke(null);
        }

        private bool CheckNormal(Collision2D target)
        {
            foreach (var contact in target.contacts)
            {
                if (contact.normal == Vector2.up)
                    return true;
            }

            return false;
        }
    }
}