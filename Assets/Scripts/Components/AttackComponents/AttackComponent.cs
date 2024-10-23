using ShootEmUp.Components.Weapons;
using UnityEngine;

namespace ShootEmUp.Components.AttackComponents
{
    public class AttackComponent : MonoBehaviour
    {
        private IGun _gun;
        private IShootEvent _shootEvent;

        private void OnEnable()
        {
            if (_shootEvent != null)
                _shootEvent.Fired += _gun.Shoot;
        }

        private void OnDisable()
        {
            _shootEvent.Fired -= _gun.Shoot;
        }

        public void Initialize(IGun gun, IShootEvent shootEvent)
        {
            _gun = gun;
            _shootEvent = shootEvent;
            _shootEvent.Fired += _gun.Shoot;
        }
    }
}