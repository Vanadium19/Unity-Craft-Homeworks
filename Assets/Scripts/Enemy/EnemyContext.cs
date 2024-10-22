using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContext : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private void Initialize()
    {
        IMoveSource moveSource = gameObject.AddComponent<EnemyMoveSource>();
        gameObject.AddComponent<Mover>().Init(moveSource, _speed);
    }
}