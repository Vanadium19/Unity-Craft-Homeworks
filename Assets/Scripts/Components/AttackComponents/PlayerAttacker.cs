using UnityEngine;

namespace ShootEmUp.Components.AttackComponents
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