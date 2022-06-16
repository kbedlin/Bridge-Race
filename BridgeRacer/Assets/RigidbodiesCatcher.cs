using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodiesCatcher : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "You")
        {
            gameManager.loseGame();
        }

        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            return;
        }

        Brick brick;
        if (other.gameObject.TryGetComponent<Brick>(out brick))
        {
            brick.GoOrigin();
        }
    }
}
