using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    //Obiekt przechowuj�cy ceg�y
    Transform bricksParent;
    //Dystans, na kt�rym AI od razu rzuca si� na ceg��
    float closeDistance = 2;
    //Przechowuje kolor AI
    Color color;
    //Enumerator stan�w AI
    enum State
    {
        PickUpBricks,
        GoToStairs,
        BuildBridge
    }
    //Najpierw AI podnosi ceg�y
    State state = State.PickUpBricks;
    //Obecny punkt destynacji
    public Vector3 destination;
    //Szybko�� poruszania si�
    public float speed = 5;
    //Komponent rigidbody AI
    Rigidbody body;
    //"Plecak" cegie�ek
    Stack<Brick> bricks;
    //Wszystkie schody na poziomie
    GameObject[] stairs;
    //Zmienna pomagaj�ca w przechodzeniu na nast�pne platformy
    int floor = 0;
    //Zmienna pomagaj�ca w chodzeniu do schod�w
    bool atStairs = false;
    //Zmienna decyduj�ca czy AI mo�e si� rusza�
    bool canMove = false;

    void Start()
    {
        //Inicjacja zmiennych
        bricksParent = GameObject.Find("Bricks").transform;
        stairs = GameObject.FindGameObjectsWithTag("Stairs");

        color = this.GetComponent<Renderer>().material.color;
        body = this.GetComponent<Rigidbody>();
        bricks = this.GetComponent<BrickHolder>().bricks;

        destination = this.transform.position;
    }

    private void FixedUpdate()
    {
        //Je�li nie mo�e si� rusza� nie r�b nic
        if (!canMove)
            return;
        switch (state)
        {
            //Podnosi ceg�y a� b�dzie ich conajmniej 5
            case State.PickUpBricks:
                if (Vector3.Distance(destination, this.transform.position) < 1)
                {
                    if (bricks.Count >= 5)
                    {
                        state = State.GoToStairs;
                        break;
                    }
                    destination = FindClosePickableBrick();
                }
                Move(destination);
                break;
            //Idzie w stron� najbli�szych schod�w
            case State.GoToStairs:
                if (Vector3.Distance(destination, this.transform.position) < 1)
                {
                    if (!atStairs)
                    {
                        //Vector3.back w celu unikania kolizji
                        destination = FindBestStairs() + Vector3.back;
                        atStairs = !atStairs;
                    }
                    else
                    {
                        atStairs = !atStairs;
                        state = State.BuildBridge;
                        break;
                    }    
                }
                Move(destination);

                break;
            //Idzie naprz�d i buduje schody a� sko�cz� si� ceg�y
            case State.BuildBridge:
                if (bricks.Count != 0)
                {
                    Move(this.transform.position + Vector3.forward);
                    //Je�li zmieni si� zmienna floor przechodzi na nast�pn� platform�
                    if (this.transform.position.y >= floor * 5 + 5)
                    {
                        floor++;
                        destination = FindClosePickableBrick();
                        state = State.PickUpBricks;
                    }
                    return;
                }
                else
                {
                    state = State.PickUpBricks;
                }
                break;
        }

    }
    //Prosty skrypt poruszania si� do miejsca destynacji
    private void Move(Vector3 destination)
    {
        this.transform.LookAt(new Vector3(destination.x, this.transform.position.y, destination.z));
        body.velocity = new Vector3(transform.forward.x * speed, body.velocity.y, transform.forward.z * speed);
    }
    //Znajduje najbi�sze schody na odpowiedniej wysoko�ci
    private Vector3 FindBestStairs()
    {
        float distance = float.MaxValue;
        Vector3 stairsPos = Vector3.zero;
        foreach (var st in stairs)
        {
            if (st.transform.position.y > this.transform.position.y + 2 ||
                st.transform.position.y < this.transform.position.y - 2)
                continue;
            float dist = Vector3.Distance(st.transform.position, this.transform.position);
            if (dist < distance)
            {
                distance = dist;
                stairsPos = st.transform.position;
            }
        }
        return stairsPos;
    }
    //Znajduje pobliskie ceg�y i zwraca ich pozycj�
    private Vector3 FindClosePickableBrick()
    {
        float distance = float.MaxValue;
        float minDistance = distance;
        Vector3 brickPos = Vector3.zero;
        for (int i = 0; i < bricksParent.childCount; i++)
        {
            if ((color == bricksParent.GetChild(i).GetComponent<Renderer>().material.color ||
                color == Color.gray))
            {
                distance = (this.transform.position - bricksParent.GetChild(i).transform.position).magnitude;
            }
            //Je�li ceg�a jest w bliskim dystansie nie szuka najbli�szej tylko wybiera j� 
            if (distance <= closeDistance)
            {
                brickPos = bricksParent.GetChild(i).transform.position;
                break;
            }
            if (distance < minDistance)
            {
                minDistance = distance;
                brickPos = bricksParent.GetChild(i).transform.position;
            }
        }
        return brickPos;
    }

    private void OnCollisionStay(Collision collision)
    {
        //Mo�e si� porusza� kiedy jest w kolizji i t� kolizj� nie jest inny gracz
        if (collision.gameObject.tag != "Player")
        {
            canMove = true;
            return;
        }
        //Je�li uderzy w innego gracza wy��cza poruszanie si� na chwil�,
        //w za�o�eniu, �e ten obiekt oderwie si� od ziemii
        canMove = false;
        Invoke("MoveAgain", 1);
    }

    private void OnCollisionExit(Collision collision)
    {
        //Gdy obiekt nie koliduje z niczym nie mo�e si� rusza�,
        canMove = false;
    }
    //Ponownie si� poruszaj
    private void MoveAgain()
    {
        canMove = true;
    }
}
