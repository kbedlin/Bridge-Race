using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepInstantiating : MonoBehaviour
{
    public GameObject step;
    public List<GameObject> steps;

    int iter = 0;
    readonly Vector2 stepEdge = new Vector2(0.173205f, 0.3f);

    private void OnCollisionStay(Collision collision)
    {
        Vector3 newStep = new Vector3(this.transform.position.x,
            iter * stepEdge.x - stepEdge.x / 2,
            this.transform.position.z + iter * stepEdge.y + stepEdge.y / 2);

        if (collision.transform.position.z > newStep.z - stepEdge.y / 2 &&
            collision.transform.position.z < newStep.z + stepEdge.y / 2)
        {
            GameObject stp = Instantiate(step, newStep, Quaternion.identity);
            stp.GetComponent<Renderer>().material = collision.gameObject.GetComponent<Renderer>().material;
            steps.Add(stp);
            iter++;
        }

    }
}
