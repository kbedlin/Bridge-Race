using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public delegate void EndGame(string winner);
    public delegate void LoseGame();

    public EndGame endGame;
    public LoseGame loseGame;

}
