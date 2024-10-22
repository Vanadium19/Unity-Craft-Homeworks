using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour, IGun
{
    public void Shoot()
    {
        Debug.Log("Выстрел противника!");
    }
}