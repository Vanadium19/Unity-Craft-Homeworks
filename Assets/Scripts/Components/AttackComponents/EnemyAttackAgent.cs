using UnityEngine;

namespace ShootEmUp.Components.AttackComponents
{
    public class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private float _delay = 1f;

        private float _currentTime;

        private void Start()
        {
            _currentTime = _delay;
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime > 0)
                return;

            Shoot();
        }

        private void Shoot()
        {
            Debug.Log("Выстрел противника!");
            _currentTime = _delay;
        }
    }
}