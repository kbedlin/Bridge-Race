using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepInstantiating : MonoBehaviour
{
    public GameObject step; //Step prefab
    public List<GameObject> steps = new List<GameObject>(); //List of all steps in stairs

    int iter = 0; //Last step
    readonly Vector2 stepSize = new Vector2(0.173205f, 0.3f);

    private void OnCollisionStay(Collision collision)
    {
        //Position of the next step that will be added to the stairs
        Vector3 newStep = new Vector3(this.transform.position.x,
            iter * stepSize.x - stepSize.x / 2,
            this.transform.position.z + iter * stepSize.y + stepSize.y / 2);
        Stack<Brick> bricks = collision.gameObject.GetComponent<BrickHolder>().bricks;
        if (bricks.Count != 0)
        {
            if (collision.transform.position.z > newStep.z - stepSize.y / 2 &&
                collision.transform.position.z < newStep.z + stepSize.y / 2)
            {
                //Creates a step, sets it's color and adds to the list
                GameObject stp = Instantiate(step, newStep, Quaternion.identity);
                stp.transform.parent = this.transform;
                stp.GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Renderer>().material.color;
                steps.Add(stp);
                iter++;
                Brick brick = bricks.Pop();
                brick.transform.parent = null;
                brick.transform.position = brick.originPosition;
                brick.transform.rotation = Quaternion.identity;
            }
            else //If the character of different color gets on already created step change that step's color
            {

                GameObject stepBelow = FindTheStepBelow(collision);
                if (stepBelow != null)
                {
                    var stepMaterial = stepBelow.GetComponent<Renderer>().material;
                    var collisionMaterial = collision.gameObject.GetComponent<Renderer>().material;
                    if (stepMaterial.color != collisionMaterial.color)
                    {
                        stepMaterial.color = collisionMaterial.color;
                        Brick brick = bricks.Pop();
                        brick.transform.parent = null;
                        brick.transform.position = brick.originPosition;
                        brick.transform.rotation = Quaternion.identity;
                    }
                }
            }
            
        }
    }

    private GameObject FindTheStepBelow(Collision collision) //Finds the step directly below the character
    {
        foreach (var step in steps)
        {
            Vector2 stepBorders = new Vector2(step.transform.position.z - stepSize.y / 2,
                step.transform.position.z + stepSize.y / 2);
            if (collision.transform.position.z > stepBorders.x &&
                collision.transform.position.z < stepBorders.y)
            {
                return step; 
            }
                
        }
        return null;
    }
}
