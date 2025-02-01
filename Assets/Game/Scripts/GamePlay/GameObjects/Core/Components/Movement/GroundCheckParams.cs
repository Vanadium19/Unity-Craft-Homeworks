using System;
using UnityEngine;

namespace Game.Core.Components
{
    [Serializable]
    public struct GroundCheckParams
    {
        [SerializeField] private Transform _point;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Vector2 _overlapSize;

        public Transform Point => _point;
        public LayerMask GroundLayer => _groundLayer;
        public Vector2 OverlapSize => _overlapSize;
    }
}