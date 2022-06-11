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

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.GetComponent<Renderer>().material.color == originColor ||
            this.GetComponent<Renderer>().material.color == Color.gray) &&
            collision.gameObject.tag == "Player")
        {
            this.transform.parent = collision.gameObject.transform;
            this.transform.rotation = collision.transform.rotation;
            this.transform.localPosition = Vector3.back / 2 + new Vector3(0, 0.101f, 0) * collision.gameObject.transform.childCount;
            collision.gameObject.GetComponent<BrickHolder>().bricks.Push(this);
        }
    }
}
