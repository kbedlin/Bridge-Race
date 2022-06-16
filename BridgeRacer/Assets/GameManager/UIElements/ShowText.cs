using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.endGame += Show;
    }

    private void Show(string winner)
    {
        Text text = this.GetComponent<Text>();
        text.text = winner + " won!";
        text.enabled = true;
    }
}
