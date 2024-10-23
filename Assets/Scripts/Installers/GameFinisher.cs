using System;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private PlayerInstaller _player;

    private void OnEnable()
    {
        _player.HealthComponent.Died += EndGame;
    }

    private void OnDisable()
    {
        _player.HealthComponent.Died -= EndGame;
    }

    private void EndGame(Health health)
    {
        health.ResetHealth();
        Time.timeScale = 0f;
    }
}