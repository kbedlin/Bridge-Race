using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();    
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
