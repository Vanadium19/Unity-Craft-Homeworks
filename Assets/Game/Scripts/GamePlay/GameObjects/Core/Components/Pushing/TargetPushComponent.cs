using UnityEngine;

namespace Game.Core.Components
{
    public class TargetPushComponent : EntityComponent
    {
        private readonly float _force;

        public TargetPushComponent(float force)
        {
            _force = force;
        }

        public bool Push(Collider2D collider, Vector2 direction)
        {
            if (!collider.TryGetComponent(out IEntity entity))
                return false;

            if (!entity.TryGet(out IPushable target))
                return false;

            target.AddForce(direction * _force);
            return true;
        }
    }
}