using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHolder : MonoBehaviour
{
    public Stack<Brick> bricks = new Stack<Brick>();
    Transform bricksStash;

    float pushUpForce = 30;
    float pushBackForce = 40;

    float brickPushForce = 6;
    int losingBricks = 3;

    private void Start()
    {
        bricksStash = GameObject.Find("Bricks").transform;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (bricks.Count < collision.gameObject.GetComponent<BrickHolder>().bricks.Count)
            {
                this.transform.LookAt(collision.gameObject.transform.position + Vector3.up / 2);
                this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * pushBackForce + Vector3.up * pushUpForce, ForceMode.Impulse);
                for (int i = 0; i < Mathf.Min(losingBricks, bricks.Count); i++)
                {
                    Brick brick = bricks.Pop();
                    brick.GetComponent<Renderer>().material.color = Color.gray;
                    brick.gameObject.AddComponent<Rigidbody>();
                    brick.transform.parent = bricksStash;
                    brick.GetComponent<Collider>().isTrigger = false;
                    brick.GetComponent<Rigidbody>().AddRelativeForce((Vector3.back + Vector3.up) * brickPushForce, ForceMode.Impulse);
                }
            }
        }
    }

}
