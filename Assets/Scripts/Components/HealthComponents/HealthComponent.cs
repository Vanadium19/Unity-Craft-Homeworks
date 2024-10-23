using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private int _health;
    private bool _isEnemy;
    private int _startHealth;

    public event Action<int> HealthChanged;
    public event Action<HealthComponent> Died;

    public bool IsEnemy => _isEnemy;

    public void TakeDamage(int damage)
    {
        if (_health <= 0)
            return;

        _health = Mathf.Max(0, _health - damage);
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            Died?.Invoke(this);
    }

    public HealthComponent Initialize(int health, bool isEnemy)
    {
        _health = health;
        _startHealth = health;
        _isEnemy = isEnemy;
        return this;
    }

    public void ResetHealth()
    {
        _health = _startHealth;
    }
}