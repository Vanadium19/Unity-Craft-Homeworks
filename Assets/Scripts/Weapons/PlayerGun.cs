using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour, IGun
{
    public void Shoot()
    {
        Debug.Log("Выстрел игрока!");
    }
}