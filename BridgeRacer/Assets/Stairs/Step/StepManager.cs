using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepManager : MonoBehaviour
{
    //Prefab schodka
    public GameObject step; 
    //Lista schodk�w w schodach
    public List<GameObject> steps = new List<GameObject>(); 
    //Indeks ostatniego schodka
    int iter = 0;
    //Rozmiar schodka y i z, obliczona z tr�jk�ta prostok�tnego jaki tworzy p�aszczyzna schod�w
    readonly Vector2 stepSize = new Vector2(0.173205f, 0.3f);
    //Si�a wypchni�cia przez schody 
    public float pushForce = 60;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        //Pozycja nast�pnego schodka, kt�ry zosta�by dodany do schod�w
        Vector3 newStep = new Vector3(this.transform.position.x,
            iter * stepSize.x - stepSize.x / 2 + this.transform.position.y,
            this.transform.position.z + iter * stepSize.y + stepSize.y / 2);
        //Pobiera stos cegie� z BrickHoldera gracza
        Stack<Brick> bricks = collision.gameObject.GetComponent<BrickHolder>().bricks;
        if (bricks.Count != 0)
        {
            //Je�li gracz jest w pozycji nowego schodka, instancjuje go, ustawia jego kolor i dodaje do listy schodk�w
            if (collision.transform.position.z > newStep.z - stepSize.y / 2 &&
                collision.transform.position.z < newStep.z + stepSize.y / 2)
            {
                GameObject stp = Instantiate(step, newStep, Quaternion.identity);
                stp.transform.parent = this.transform.parent;
                stp.GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Renderer>().material.color;
                steps.Add(stp);
                iter++;
                ReturnBrick(bricks);
            }
            //Zmienia kolor schodka po wej�ciu na niego gracza o innym kolorze
            else
            {

                GameObject stepBelow = FindTheStepBelow(collision.transform.position);
                if (stepBelow != null)
                {
                    var stepMaterial = stepBelow.GetComponent<Renderer>().material;
                    var collisionMaterial = collision.gameObject.GetComponent<Renderer>().material;
                    if (stepMaterial.color != collisionMaterial.color)
                    {
                        stepMaterial.color = collisionMaterial.color;
                        ReturnBrick(bricks);
                    }
                }
            }
            
        }
        //Je�li gracz pr�buje bez cegie�ek wej�� na schodek o innym kolorze, jest wypychany
        else
        {
            GameObject stepBelow = FindTheStepBelow(collision.transform.position + new Vector3(0, 0, stepSize.y));
            if (stepBelow == null)
            {
                return;
            }
            if (stepBelow.GetComponent<Renderer>().material.color != collision.gameObject.GetComponent<Renderer>().material.color)
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -pushForce), ForceMode.Impulse);
            }
        }
    }

    //Szuka schodka bezpo�rednio pod podan� pozycj�
    private GameObject FindTheStepBelow(Vector3 position)
    {
        foreach (var step in steps)
        {
            Vector2 stepBorders = new Vector2(step.transform.position.z - stepSize.y / 2,
                step.transform.position.z + stepSize.y / 2);
            if (position.z > stepBorders.x &&
                position.z < stepBorders.y)
            {
                return step; 
            }
                
        }
        return null;
    }
    //Zwraca cegie�k� na pierwotn� pozycj�
    private void ReturnBrick(Stack<Brick> bricks)
    {
        Brick brick = bricks.Pop();
        brick.GoOrigin();
    }
}
