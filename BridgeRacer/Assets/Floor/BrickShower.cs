using UnityEngine;

public class BrickShower : MonoBehaviour
{
    Brick[,] brickMatrix;

    private void Start()
    {
        brickMatrix = GetComponent<BrickSpawner>().brickMatrix;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (var brick in brickMatrix)
            {
                if (brick.GetComponent<Renderer>().material.color == collision.gameObject.GetComponent<Renderer>().material.color)
                    brick.gameObject.SetActive(true);
            }
            bool allActive = true;
            foreach (var brick in brickMatrix)
            {
                if (brick.gameObject.activeSelf == false)
                {
                    allActive = false;
                    break;
                }
            }
            if (allActive)
                Destroy(GetComponent<BrickShower>());
        }
    }
}
