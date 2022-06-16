using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    Transform bricksParent;
    float closeDistance = 2;
    Color color;

    enum State
    {
        PickUpBricks,
        GoToStairs,
        BuildBridge
    }

    State state = State.PickUpBricks;
    public Vector3 destination;

    public float speed = 5;

    Rigidbody body;

    Stack<Brick> bricks;

    GameObject[] stairs;

    int floor = 0;

    bool atStairs = false;
    bool canMove = false;

    void Start()
    {
        bricksParent = GameObject.Find("Bricks").transform;
        stairs = GameObject.FindGameObjectsWithTag("Stairs");

        color = this.GetComponent<Renderer>().material.color;
        body = this.GetComponent<Rigidbody>();
        bricks = this.GetComponent<BrickHolder>().bricks;

        destination = this.transform.position;
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;
        switch (state)
        {
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
            case State.GoToStairs:
                if (Vector3.Distance(destination, this.transform.position) < 1)
                {
                    if (!atStairs)
                    {
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
            case State.BuildBridge:
                if (bricks.Count != 0)
                {
                    Move(this.transform.position + Vector3.forward);
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

    private void Move(Vector3 destination)
    {
        this.transform.LookAt(new Vector3(destination.x, this.transform.position.y, destination.z));
        body.velocity = new Vector3(transform.forward.x * speed, body.velocity.y, transform.forward.z * speed);
    }

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
        if (collision.gameObject.tag != "Player")
        {
            canMove = true;
            return;
        }

        canMove = false;
        Invoke("MoveAgain", 1);
    }

    private void OnCollisionExit(Collision collision)
    {
        canMove = false;
    }

    private void MoveAgain()
    {
        canMove = true;
    }
}
