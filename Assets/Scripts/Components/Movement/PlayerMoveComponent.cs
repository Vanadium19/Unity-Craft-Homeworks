using UnityEngine;

namespace ShootEmUp.Components.Movement
{
    [RequireComponent(typeof(Mover))]
    public class PlayerMoveComponent : MonoBehaviour
    {
        private readonly string _axisName = "Horizontal";

        private Mover _mover;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var direction = Vector2.right * Input.GetAxis(_axisName);

            _mover.Move(direction);
        }
    }
}