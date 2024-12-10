using Modules;
using UnityEngine;

namespace PlayerInput
{
    public class MoveInput : IMoveInput
    {
        public SnakeDirection GetDirection()
        {
            if (Input.GetKey(KeyCode.W))
                return SnakeDirection.UP;
            else if (Input.GetKey(KeyCode.S))
                return SnakeDirection.DOWN;
            else if (Input.GetKey(KeyCode.A))
                return SnakeDirection.LEFT;
            else if (Input.GetKey(KeyCode.D))
                return SnakeDirection.RIGHT;

            return SnakeDirection.NONE;
        }
    }
}