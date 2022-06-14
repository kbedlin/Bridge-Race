using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    GameObject[] floors;
    GameObject finish;

    private void Start()
    {
        floors = GameObject.FindGameObjectsWithTag("Floor");
        finish = GameObject.FindGameObjectWithTag("Finish");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<BrickHolder>().bricks.Count > 0)
            {
                this.transform.Translate(Vector3.down * 0.3f);
                CheckIfShouldBeDestroyed();
            }
        }
    }

    private void CheckIfShouldBeDestroyed()
    {
        foreach (var floor in floors)
        {
            if (this.transform.position.z >= floor.transform.position.z - floor.transform.lossyScale.z * 10 / 2 &&
                this.transform.position.z <= floor.transform.position.z + floor.transform.lossyScale.z * 10 / 2)
                Destroy(this.gameObject);
        }
        if (this.transform.position.z >= finish.transform.position.z - finish.transform.lossyScale.z / 2 &&
            this.transform.position.z <= finish.transform.position.z + finish.transform.lossyScale.z / 2)
            Destroy(this.gameObject);
    }
}
