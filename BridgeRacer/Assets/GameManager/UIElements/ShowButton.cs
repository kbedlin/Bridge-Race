using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowButton : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.endGame += Show;
        gameManager.loseGame += Show;
        Debug.Log("cos");

    }

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
