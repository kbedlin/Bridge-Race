using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            Destroy(this);
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<BrickHolder>().bricks.Count > 0)
            {
                this.transform.position += new Vector3(0, 0, 0.3f);
            }
        }
    }
}
