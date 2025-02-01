using UnityEngine;

namespace Game.Core.Components
{
    public interface IPushable
    {
        public void AddForce(Vector2 force);
    }
}