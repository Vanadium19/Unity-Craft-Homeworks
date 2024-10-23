using ShootEmUp.Components.HealthComponents;
using ShootEmUp.Installers;
using UnityEngine;

namespace ShootEmUp.Level
{
    public class GameFinisher : MonoBehaviour
    {
        [SerializeField] private PlayerInstaller _player;

        private void OnEnable()
        {
            _player.Health.Died += EndGame;
        }

        private void OnDisable()
        {
            _player.Health.Died -= EndGame;
        }

        private void EndGame(Health health)
        {
            health.ResetHealth();
            Time.timeScale = 0f;
        }
    }
}