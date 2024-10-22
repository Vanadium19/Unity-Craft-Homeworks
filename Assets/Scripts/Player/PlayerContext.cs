using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private void Initialize()
    {
        IMoveSource moveSource = gameObject.AddComponent<PlayerMoveSource>();
        gameObject.AddComponent<Mover>().Init(moveSource, _speed);
    }
}