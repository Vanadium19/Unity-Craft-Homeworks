using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _health;
    private bool _isEnemy;

    public event Action<int> HealthChanged;
    public event Action Died;

    public bool IsEnemy => _isEnemy;

    public void TakeDamage(int damage)
    {
        if (_health <= 0)
            return;

        _health = Mathf.Max(0, _health - damage);
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            Died?.Invoke();
    }

    public void Initialize(int health, bool isEnemy)
    {
        _health = health;
        _isEnemy = isEnemy;
    }
}