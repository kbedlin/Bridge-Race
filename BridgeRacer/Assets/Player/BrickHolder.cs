using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHolder : MonoBehaviour
{
    public Stack<Brick> bricks = new Stack<Brick>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }

    private void applyForce(Collision collision)
    {

    }
}
