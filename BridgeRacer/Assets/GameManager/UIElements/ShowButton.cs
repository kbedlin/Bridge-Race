using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowButton : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        //Dodaje metody do delegat
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.endGame += Show;
        gameManager.loseGame += Show;

    }
    //Przeci¹¿ona metoda dodawana do delegat endGame i loseGame, w³¹czaj¹ca odpowiednie przyciski
    private void Show(string winner)
    {
        this.GetComponent<Image>().enabled = true;
        this.GetComponent<Button>().enabled = true;
        this.transform.GetChild(0).GetComponent<Text>().enabled = true;
    }

    private void Show()
    {
        this.GetComponent<Image>().enabled = true;
        this.GetComponent<Button>().enabled = true;
        this.transform.GetChild(0).GetComponent<Text>().enabled = true;
    }
}
