using System;
using UnityEngine;

public class PlayerShootEvent : MonoBehaviour, IShootEvent
{
    public event Action Fired;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Fired?.Invoke();
    }
}