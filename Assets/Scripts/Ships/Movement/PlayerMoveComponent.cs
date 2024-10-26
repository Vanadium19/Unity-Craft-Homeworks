using UnityEngine;

namespace ShootEmUp.Ships.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMoveComponent : MonoBehaviour
    {
        private readonly string _axisName = "Horizontal";

        [SerializeField] private float _speed = 5f;

        private Mover _mover;

        private void Awake()
        {
            var rigidbody = GetComponent<Rigidbody2D>();

            _mover = new Mover(_speed, rigidbody);
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