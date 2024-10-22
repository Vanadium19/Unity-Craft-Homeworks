using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private int _health = 5;
    [SerializeField] private float _speed = 5.0f;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameObject.AddComponent<Health>().Initialize(_health);

        IMoveSource moveSource = gameObject.AddComponent<PlayerMoveSource>();
        gameObject.AddComponent<Mover>().Initialize(moveSource, _speed);

        IGun gun = gameObject.AddComponent<PlayerGun>();
        IShootEvent shootEvent = gameObject.AddComponent<PlayerShootEvent>();
        gameObject.AddComponent<AttackComponent>().Initialize(gun, shootEvent);
    }
}