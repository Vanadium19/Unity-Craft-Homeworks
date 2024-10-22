using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootEvent : MonoBehaviour, IShootEvent
{
    private float _currentTime;
    private float _delay;

    public event Action Fired;

    private void Update()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime > 0)
            return;

        Fired?.Invoke();
        _currentTime = _delay;
    }

    public IShootEvent Initialize(float delay)
    {
        _delay = delay;
        _currentTime = delay;

        return this;
    }
}