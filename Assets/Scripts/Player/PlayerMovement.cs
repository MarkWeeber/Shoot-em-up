using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbody2d;

    private void Start()
    {
        rbody2d = GetComponent<Rigidbody2D>();
    }
    
    public void Move(Vector2 direction)
    {
        rbody2d.velocity = direction;
    }

    public void Dash(Vector2 direction)
    {
        if(direction == Vector2.zero)
        {
            
        }
        else
        {
            
        }
    }
}
