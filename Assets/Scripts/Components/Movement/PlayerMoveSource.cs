using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSource : MonoBehaviour, IMoveSource
{
    private Vector2 _value;

    public Vector2 Value => _value;

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            _value = Vector2.left;
        else if (Input.GetKey(KeyCode.RightArrow))
            _value = Vector2.right;
        else
            _value = Vector2.zero;
    }
}