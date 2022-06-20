using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHolder : MonoBehaviour
{
    //Stos cegie�ek w "plecaku"
    public Stack<Brick> bricks = new Stack<Brick>();
    //Obiekt, kt�ry przechowuje ceg�y nietrzymane przez nikogo
    Transform bricksStash;
    //Si�y odepchni�cia podczas zderzenia
    float pushUpForce = 30;
    float pushBackForce = 40;
    //Si�a odepchni�cia cegie�ek podczas zderzenia
    float brickPushForce = 6;
    //Maksymalna ilo�� gubionych cegie� na kolizj�
    int losingBricks = 3;

    private void Start()
    {
        bricksStash = GameObject.Find("Bricks").transform;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Je�li masz mniej cegie� jeste� odpychany i tracisz ceg�y
            if (bricks.Count < collision.gameObject.GetComponent<BrickHolder>().bricks.Count)
            {
                this.transform.LookAt(collision.gameObject.transform.position + Vector3.up / 2);
                this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * pushBackForce + Vector3.up * pushUpForce, ForceMode.Impulse);
                for (int i = 0; i < Mathf.Min(losingBricks, bricks.Count); i++)
                {
                    Brick brick = bricks.Pop();
                    //Wyrzuca ceg�y, ustawia ich kolor na szary, dodaje im rigidbody,
                    //dodaje je do obiektu bricksStash i wy��cza trigger
                    brick.GetComponent<Renderer>().material.color = Color.gray;
                    brick.gameObject.AddComponent<Rigidbody>();
                    brick.transform.parent = bricksStash;
                    brick.GetComponent<Collider>().isTrigger = false;
                    brick.GetComponent<Rigidbody>().AddRelativeForce((Vector3.back + Vector3.up) * brickPushForce, ForceMode.Impulse);
                }
                return;
            }
            //Je�li masz tyle samo cegie� co kolizja odpycha Ci� troch� s�abiej,
            //to ma zapobiega� utykaniu postaci ze sob�
            if (bricks.Count == collision.gameObject.GetComponent<BrickHolder>().bricks.Count)
            {
                this.transform.LookAt(collision.gameObject.transform.position + Vector3.up / 2);
                this.GetComponent<Rigidbody>().AddRelativeForce((Vector3.back * pushBackForce + Vector3.up * pushUpForce) / 3, ForceMode.Impulse);
            }
        }
    }

}
