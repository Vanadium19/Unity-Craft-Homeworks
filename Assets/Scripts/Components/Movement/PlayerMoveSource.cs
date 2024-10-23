using UnityEngine;

namespace ShootEmUp.Components.Movement
{
    public class PlayerMoveSource : MonoBehaviour, IMoveSource
    {
        private readonly string _axisName = "Horizontal";

        private Vector2 _value;

        public Vector2 Value => _value;

        private void Update()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            _value = Vector2.right * Input.GetAxis(_axisName);
        }
    }
}