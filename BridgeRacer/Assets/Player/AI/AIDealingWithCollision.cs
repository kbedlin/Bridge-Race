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

    private void OnCollisionEnter(Collision collision)
    {

        if (!(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Stairs"))
            return;

        if (this.transform.position.x < collision.gameObject.transform.position.x)
        {
            body.velocity = Vector3.right * speed;
        }
        else
        {
            body.velocity = Vector3.left * speed;
        }
    }
}
