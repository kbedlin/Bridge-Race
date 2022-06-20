using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        //Dodaje metody do delegat
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.endGame += Show;
        gameManager.loseGame += Show;
    }
    //Przeci¹¿ona metoda dodawana do delegat wyœwietlaj¹ca napisy koñcowe
    private void Show(string winner)
    {
        Text text = this.GetComponent<Text>();
        text.text = winner + " won!";
        text.enabled = true;
    }

    private void Show()
    {
        Text text = this.GetComponent<Text>();
        text.text = "You lost!";
        text.enabled = true;
    }
}
