using UnityEngine;

namespace Game.Components
{
    public class GroundChecker
    {
        private const float OverlapAngle = 0;

        private readonly Transform _jumpPoint;
        private readonly Vector2 _overlapSize;
        private readonly int _layerMask;

        public GroundChecker(GroundCheckParams checkParams)
        {
            _jumpPoint = checkParams.Point;
            _overlapSize = checkParams.OverlapSize;
            _layerMask = checkParams.GroundLayer;
        }

        public bool IsGrounded => Physics2D.OverlapBox(_jumpPoint.position, _overlapSize, OverlapAngle, _layerMask);
    }
}