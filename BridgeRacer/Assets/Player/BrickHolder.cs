using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHolder : MonoBehaviour
{
    //Stos cegie³ek w "plecaku"
    public Stack<Brick> bricks = new Stack<Brick>();
    //Obiekt, który przechowuje ceg³y nietrzymane przez nikogo
    Transform bricksStash;
    //Si³y odepchniêcia podczas zderzenia
    float pushUpForce = 30;
    float pushBackForce = 40;
    //Si³a odepchniêcia cegie³ek podczas zderzenia
    float brickPushForce = 6;
    //Maksymalna iloœæ gubionych cegie³ na kolizjê
    int losingBricks = 3;

    private void Start()
    {
        bricksStash = GameObject.Find("Bricks").transform;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Jeœli masz mniej cegie³ jesteœ odpychany i tracisz ceg³y
            if (bricks.Count < collision.gameObject.GetComponent<BrickHolder>().bricks.Count)
            {
                this.transform.LookAt(collision.gameObject.transform.position + Vector3.up / 2);
                this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * pushBackForce + Vector3.up * pushUpForce, ForceMode.Impulse);
                for (int i = 0; i < Mathf.Min(losingBricks, bricks.Count); i++)
                {
                    Brick brick = bricks.Pop();
                    //Wyrzuca ceg³y, ustawia ich kolor na szary, dodaje im rigidbody,
                    //dodaje je do obiektu bricksStash i wy³¹cza trigger
                    brick.GetComponent<Renderer>().material.color = Color.gray;
                    brick.gameObject.AddComponent<Rigidbody>();
                    brick.transform.parent = bricksStash;
                    brick.GetComponent<Collider>().isTrigger = false;
                    brick.GetComponent<Rigidbody>().AddRelativeForce((Vector3.back + Vector3.up) * brickPushForce, ForceMode.Impulse);
                }
                return;
            }
            //Jeœli masz tyle samo cegie³ co kolizja odpycha Ciê trochê s³abiej,
            //to ma zapobiegaæ utykaniu postaci ze sob¹
            if (bricks.Count == collision.gameObject.GetComponent<BrickHolder>().bricks.Count)
            {
                this.transform.LookAt(collision.gameObject.transform.position + Vector3.up / 2);
                this.GetComponent<Rigidbody>().AddRelativeForce((Vector3.back * pushBackForce + Vector3.up * pushUpForce) / 3, ForceMode.Impulse);
            }
        }
    }

}
