using ShootEmUp.Components.HealthComponents;
using ShootEmUp.Installers;
using UnityEngine;

namespace ShootEmUp.Level
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerInstaller _player;
        [SerializeField] private Transform _bulletWorldContainer;
        [SerializeField] private EnemySpawner _spawner;

        private void Awake()
        {
            _player.Initialize(false, _bulletWorldContainer);
            _spawner.Initialize(_bulletWorldContainer, _player.transform);
        }

        private void OnEnable()
        {
            _player.Health.Died += EndGame;
        }

        private void Start()
        {
            _spawner.StartSpawn();
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