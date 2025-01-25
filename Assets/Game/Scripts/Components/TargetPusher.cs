using UnityEngine;

namespace Game.Components
{
    public class TargetPusher
    {
        private readonly float _force;

        public TargetPusher(float force)
        {
            _force = force;
        }

        public void Push(Collider2D collider, Vector2 direction)
        {
            if (collider.TryGetComponent(out IPushable pushable))
                pushable.AddForce(direction * _force);
        }
    }
}