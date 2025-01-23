using System;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public struct JumpParams
    {
        [SerializeField] private float _force;
        [SerializeField] private float _delay;
        [SerializeField] private Transform _point;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Vector2 _overlapSize;

        public float Force => _force;
        public float Delay => _delay;
        public Transform Point => _point;
        public LayerMask GroundLayer => _groundLayer;
        public Vector2 OverlapSize => _overlapSize;
    }
}