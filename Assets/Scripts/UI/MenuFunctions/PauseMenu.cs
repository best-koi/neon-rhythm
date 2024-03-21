using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static Action onPauseMenuEscape;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void MainMenu()
    {
        onPauseMenuEscape();
        SceneManager.LoadScene("SceneManagement", LoadSceneMode.Single);   
    }

    public void Resume()
    {
        onPauseMenuEscape();
        SceneManager.UnloadSceneAsync("PauseMenu");
    } 
}
