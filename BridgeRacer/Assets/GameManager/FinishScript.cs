using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        //Dodaje metody do delegat endGame i loseGame
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.endGame += StopTime;
        gameManager.loseGame += StopTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Jeœli jakiœ gracz dojdzie na finishuj¹c¹ plaformê zakoñcz grê
        if (collision.gameObject.tag != "Player")
            return;
        gameManager.endGame(collision.gameObject.name);
    }
    //Przeci¹¿ona metoda zatrzymuj¹ca zatrzymuj¹ca czas
    private void StopTime(string winner)
    {
        Time.timeScale = 0;
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }
}
