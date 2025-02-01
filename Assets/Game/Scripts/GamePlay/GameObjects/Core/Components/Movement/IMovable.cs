using UnityEngine;

namespace Game.Core.Components
{
    public interface IMovable
    {
        public Vector2 Position { get; }
        
        public void Move(Vector2 direction);
    }
}