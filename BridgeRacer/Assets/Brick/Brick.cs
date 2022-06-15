using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Vector3 originPosition;
    public Color originColor;

    private void Start()
    {
        originPosition = this.transform.position;
        originColor = this.GetComponent<Renderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Renderer>().material.color == originColor &&
            other.gameObject.tag == "Player")
        {
            PushBrick(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.GetComponent<Renderer>().material.color == Color.gray &&
            collision.gameObject.tag == "Player")
        {
            PushBrick(collision.gameObject);
            this.GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Renderer>().material.color;
            this.GetComponent<Collider>().isTrigger = true;
            Destroy(this.GetComponent<Rigidbody>());
        }
    }

    private void PushBrick(GameObject collider)
    {
        this.transform.parent = collider.transform;
        this.transform.rotation = collider.transform.rotation;
        this.transform.localPosition = Vector3.back / 1.5f + new Vector3(0, 0.105f, 0) * collider.gameObject.transform.childCount;
        collider.gameObject.GetComponent<BrickHolder>().bricks.Push(this);
    }
}
