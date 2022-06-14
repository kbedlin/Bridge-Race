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
    public Vector3 startingPosition;

    float speed = 5;

    Rigidbody body;

    Stack<Brick> bricks;

    GameObject[] stairs;

    bool atStairs = false;

    void Start()
    {
        bricksParent = GameObject.Find("Bricks").transform;
        stairs = GameObject.FindGameObjectsWithTag("Stairs");

        color = this.GetComponent<Renderer>().material.color;
        body = this.GetComponent<Rigidbody>();
        bricks = this.GetComponent<BrickHolder>().bricks;

        destination = this.transform.position;
        startingPosition = this.transform.position;
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (state)
        {
            case State.PickUpBricks:
                if (Vector3.Distance(destination, this.transform.position) < 1f)
                {
                    if (bricks.Count >= 5)
                    {
                        state = State.GoToStairs;
                        break;
                    }
                    destination = FindClosePickableBrick();
                    startingPosition = this.transform.position;
                }
                Move(destination, startingPosition);
                break;
            case State.GoToStairs:
                if (Vector3.Distance(destination, this.transform.position) < 1.5f)
                {
                    if (!atStairs)
                    {
                        destination = FindBestStairs();
                        startingPosition = this.transform.position;
                        atStairs = !atStairs;
                    }
                    else
                    {
                        atStairs = !atStairs;
                        state = State.BuildBridge;
                        break;
                    }    
                }
                Move(destination, startingPosition);

                break;
            case State.BuildBridge:
                break;
        }

    }

    private void Move(Vector3 destination, Vector3 position)
    {
        body.velocity = (destination - position).normalized * speed;

        this.transform.rotation = Quaternion.LookRotation(
                new Vector3(
                    body.velocity.x,
                    0,
                    body.velocity.z));
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
                color == Color.gray) &&
                bricksParent.GetChild(i).gameObject.activeSelf)
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
}
