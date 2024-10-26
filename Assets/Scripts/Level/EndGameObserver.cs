using ShootEmUp.Ships;
using UnityEngine;

namespace ShootEmUp.Level
{
    public class EndGameObserver : MonoBehaviour
    {
        [SerializeField] private Ship _player;

        private void OnEnable()
        {
            _player.OnShipDestroyed += EndGame;
        }

        private void EndGame(Ship ship)
        {
            Time.timeScale = 0f;
            ship.OnShipDestroyed -= EndGame;
        }
    }
}