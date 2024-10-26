using ShootEmUp.Ships;
using System;
using UnityEngine;

[Serializable]
public class AttackAgent
{
    [SerializeField] private float _shootDelay = 1f;

    private float _currentTime;

    public event Action Fired;

    public void Update()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime > 0)
            return;

        _currentTime = _shootDelay;
        Fired?.Invoke();
    }
}