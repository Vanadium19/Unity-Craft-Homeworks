using UnityEngine;

namespace Game.Components
{
    public interface IPushable
    {
        public void AddForce(Vector2 force);
    }
}