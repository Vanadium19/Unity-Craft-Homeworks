using ShootEmUp.Ships;
using UnityEngine;

namespace ShootEmUp.Level
{
    public class EndGameObserver : MonoBehaviour
    {
        [SerializeField] private Ship _ship;

        private void OnEnable()
        {
            _ship.Died += EndGame;
        }

        private void EndGame(Ship ship)
        {
            Time.timeScale = 0f;
            _ship.Died -= EndGame;
        }
    }
}