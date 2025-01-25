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
            {
                Debug.Log("Pushing");
                pushable.AddForce(direction * _force);
                return;
            }
            
            if (collider.TryGetComponent(out Rigidbody2D target))
            {
                target.velocity = Vector2.zero;
                target.AddForce(direction * _force, ForceMode2D.Impulse);
            }
        }
    }
}