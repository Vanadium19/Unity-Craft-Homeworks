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

        private void OnDisable()
        {
            _ship.Died -= EndGame;
        }

        private void EndGame()
        {
            Time.timeScale = 0f;
        }
    }
}