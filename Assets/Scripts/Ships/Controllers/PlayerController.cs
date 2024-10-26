using ShootEmUp.Level.Spawners;
using UnityEngine;

namespace ShootEmUp.Ships.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private readonly string _axisName = "Horizontal";

        [SerializeField] private Ship _ship;
        [SerializeField] private BulletSpawner _bulletSpawner;

        private void Awake()
        {
            _ship.Initialize(_bulletSpawner);
        }

        private void Update()
        {
            var direction = Vector2.right * Input.GetAxis(_axisName);

            _ship.Move(direction);

            if (Input.GetKeyDown(KeyCode.Space))
                _ship.Shoot(Vector2.up);
        }
    }
}