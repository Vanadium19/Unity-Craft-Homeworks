using Game.Components.Interfaces;
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

        public bool Push(Collider2D collider, Vector2 direction)
        {
            if (!collider.TryGetComponent(out IPushable pushable))
                return false;

            pushable.AddForce(direction * _force);
            return true;
        }
    }
}