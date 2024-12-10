using System;
using UnityEngine;

namespace Modules
{
    //Don't modify
    public interface ISnake
    {
        /// Invokes after snake movement. Vector2 is the head position
        event Action<Vector2Int> OnMoved;

        /// Invokes when snake collides itself
        event Action OnSelfCollided;
        
        /// Returns the head position of the snake
        Vector2Int HeadPosition { get; }
        
        /// Rotates the snake towards direction
        void Turn(SnakeDirection direction);

        /// Expands the snake spine on a specified bone amount
        void Expand(int amount);

        void SetSpeed(float speed);

        void SetActive(bool active);
    }
}