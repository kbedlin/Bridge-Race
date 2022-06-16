using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.endGame += StopTime;
        gameManager.loseGame += StopTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        gameManager.endGame(collision.gameObject.name);
    }

    private void StopTime(string winner)
    {
        Time.timeScale = 0;
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }
}
