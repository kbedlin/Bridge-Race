using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHolder : MonoBehaviour
{
    public Stack<Brick> bricks = new Stack<Brick>();
    Transform bricksStash;

    float pushForce = 50;

    float brickPushForce = 1;
    int losingBricks = 3;

    private void Start()
    {
        bricksStash = GameObject.Find("Bricks").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (bricks.Count <= collision.gameObject.GetComponent<BrickHolder>().bricks.Count)
            {
                this.transform.LookAt(collision.gameObject.transform.position);
                this.GetComponent<Rigidbody>().AddForce((-transform.forward + Vector3.up) * pushForce, ForceMode.Impulse);
                for (int i = 0; i < Mathf.Min(losingBricks, bricks.Count); i++)
                {
                    Brick brick = bricks.Pop();
                    brick.GetComponent<Renderer>().material.color = Color.gray;
                    brick.gameObject.AddComponent<Rigidbody>();
                    brick.transform.parent = bricksStash;
                    brick.GetComponent<Collider>().isTrigger = false;
                    brick.GetComponent<Rigidbody>().AddForce((-transform.forward + Vector3.up) * brickPushForce, ForceMode.Impulse);
                }
            }
        }
    }

}
