using System;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public struct PushParams
    {
        [SerializeField] private float _force;
        [SerializeField] private float _delay;
        [SerializeField] private float _radius;
        [SerializeField] private Transform _point;
        [SerializeField] private LayerMask _layerMask;

        public float Force => _force;
        public float Delay => _delay;
        public float Radius => _radius;
        public Transform Point => _point;
        public LayerMask Mask => _layerMask;
    }
}