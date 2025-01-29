using UnityEngine;

namespace Game.Components.Interfaces
{
    public interface IMovable
    {
        public Vector2 Position { get; }
        
        public void Move(Vector2 direction);
    }
}