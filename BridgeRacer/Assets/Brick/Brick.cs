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
        if ((other.gameObject.GetComponent<Renderer>().material.color == originColor ||
            this.GetComponent<Renderer>().material.color == Color.gray) &&
            other.gameObject.tag == "Player")
        {
            this.transform.parent = other.gameObject.transform;
            this.transform.rotation = other.transform.rotation;
            this.transform.localPosition = Vector3.back / 2 + new Vector3(0, 0.101f, 0) * other.gameObject.transform.childCount;
            other.gameObject.GetComponent<BrickHolder>().bricks.Push(this);
        }
    }
}
