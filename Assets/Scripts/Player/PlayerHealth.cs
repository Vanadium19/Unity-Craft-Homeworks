using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 5;

    public event Action<int> HealthChanged;
    public event Action Died;

    public void TakeDamage(int damage)
    {
        if (_health <= 0)
            return;

        _health = Mathf.Max(0, _health - damage);
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            Died?.Invoke();
    }
}