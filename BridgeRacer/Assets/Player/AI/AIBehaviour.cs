using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    //Obiekt przechowuj¹cy ceg³y
    Transform bricksParent;
    //Dystans, na którym AI od razu rzuca siê na ceg³ê
    float closeDistance = 2;
    //Przechowuje kolor AI
    Color color;
    //Enumerator stanów AI
    enum State
    {
        PickUpBricks,
        GoToStairs,
        BuildBridge
    }
    //Najpierw AI podnosi ceg³y
    State state = State.PickUpBricks;
    //Obecny punkt destynacji
    public Vector3 destination;
    //Szybkoœæ poruszania siê
    public float speed = 5;
    //Komponent rigidbody AI
    Rigidbody body;
    //"Plecak" cegie³ek
    Stack<Brick> bricks;
    //Wszystkie schody na poziomie
    GameObject[] stairs;
    //Zmienna pomagaj¹ca w przechodzeniu na nastêpne platformy
    int floor = 0;
    //Zmienna pomagaj¹ca w chodzeniu do schodów
    bool atStairs = false;
    //Zmienna decyduj¹ca czy AI mo¿e siê ruszaæ
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
        //Jeœli nie mo¿e siê ruszaæ nie rób nic
        if (!canMove)
            return;
        switch (state)
        {
            //Podnosi ceg³y a¿ bêdzie ich conajmniej 5
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
            //Idzie w stronê najbli¿szych schodów
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
            //Idzie naprzód i buduje schody a¿ skoñcz¹ siê ceg³y
            case State.BuildBridge:
                if (bricks.Count != 0)
                {
                    Move(this.transform.position + Vector3.forward);
                    //Jeœli zmieni siê zmienna floor przechodzi na nastêpn¹ platformê
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
    //Prosty skrypt poruszania siê do miejsca destynacji
    private void Move(Vector3 destination)
    {
        this.transform.LookAt(new Vector3(destination.x, this.transform.position.y, destination.z));
        body.velocity = new Vector3(transform.forward.x * speed, body.velocity.y, transform.forward.z * speed);
    }
    //Znajduje najbi¿sze schody na odpowiedniej wysokoœci
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
    //Znajduje pobliskie ceg³y i zwraca ich pozycjê
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
            //Jeœli ceg³a jest w bliskim dystansie nie szuka najbli¿szej tylko wybiera j¹ 
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
        //Mo¿e siê poruszaæ kiedy jest w kolizji i t¹ kolizj¹ nie jest inny gracz
        if (collision.gameObject.tag != "Player")
        {
            canMove = true;
            return;
        }
        //Jeœli uderzy w innego gracza wy³¹cza poruszanie siê na chwilê,
        //w za³o¿eniu, ¿e ten obiekt oderwie siê od ziemii
        canMove = false;
        Invoke("MoveAgain", 1);
    }

    private void OnCollisionExit(Collision collision)
    {
        //Gdy obiekt nie koliduje z niczym nie mo¿e siê ruszaæ,
        canMove = false;
    }
    //Ponownie siê poruszaj
    private void MoveAgain()
    {
        canMove = true;
    }
}
