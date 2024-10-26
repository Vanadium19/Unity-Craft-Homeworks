using UnityEngine;

namespace ShootEmUp.Ships.AttackComponents
{
    public class PlayerAttacker : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }

        private void Shoot()
        {
            Debug.Log("Выстрел игрока!");
        }
    }
}