using UnityEngine;

namespace Game.Core.Components
{
    public class RotateComponent
    {
        private const float RightAngle = 0;
        private const float LeftAngle = 180;

        private readonly Transform _transform;

        public RotateComponent(Transform transform)
        {
            _transform = transform;
        }

        public void Rotate(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;

            float angle = direction.x > 0 ? RightAngle : LeftAngle;

            _transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}