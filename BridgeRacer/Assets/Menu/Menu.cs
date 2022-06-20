using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Zacznij grê
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    //WyjdŸ z gry
    public void Quit()
    {
        Application.Quit();    
    }
    //Wróæ do menu
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
