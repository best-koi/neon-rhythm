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
        SceneAdditives.LoadScene("SceneManagement", LoadSceneMode.Single);   
        onPauseMenuEscape();
    }

    public void Resume()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
        onPauseMenuEscape();
    } 
}
