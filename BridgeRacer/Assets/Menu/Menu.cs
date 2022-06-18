using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Zacznij gr�
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    //Wyjd� z gry
    public void Quit()
    {
        Application.Quit();    
    }
    //Wr�� do menu
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
