using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDealingWithCollision : MonoBehaviour
{
    Rigidbody body;

    float speed;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        speed = GetComponent<AIBehaviour>().speed;
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject.tag != "Wall")
        //{
        //    return;
        //}
        if (collision.gameObject.transform.position.x < this.transform.position.x)
        {
            body.velocity = Vector3.right * speed;
        }
        if (collision.gameObject.transform.position.x > this.transform.position.x)
        {
            body.velocity = Vector3.left * speed;
        }
    }
}
