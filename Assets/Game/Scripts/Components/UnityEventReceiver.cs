using System;
using UnityEngine;

namespace Game.Components
{
    public class UnityEventReceiver : MonoBehaviour
    {
        public event Action<Collider2D> OnCollisionEntered;
        public event Action<Collider2D> OnTriggerEntered;
        public event Action<Collider2D> OnCollisionExited;
        public event Action<Collider2D> OnTriggerExited;

        private void OnCollisionEnter2D(Collision2D other)
        {
            // Debug.Log("OnCollisionEnter2D");
            
            OnCollisionEntered?.Invoke(other.collider);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Debug.Log("OnTriggerEnter2D");
            
            OnTriggerEntered?.Invoke(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            OnCollisionExited?.Invoke(other.collider);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExited?.Invoke(other);
        }
    }
}