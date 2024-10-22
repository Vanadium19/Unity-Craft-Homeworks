using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _health = 5;

    public void TakeDamage(int damage)
    {
        if (_health <= 0)
            return;

        _health = Mathf.Max(0, _health - damage);
    }
}