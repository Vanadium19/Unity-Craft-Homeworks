using System;
using UnityEngine;

namespace ShootEmUp.Components.AttackComponents
{
    public class PlayerShootEvent : MonoBehaviour, IShootEvent
    {
        public event Action Fired;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Fired?.Invoke();
        }
    }
}