using System;
using UnityEngine;

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
        SceneAdditives.LoadScene("SceneManagement", UnityEngine.SceneManagement.LoadSceneMode.Single);
        onPauseMenuEscape();
    }

    public void Resume()
    {
        SceneAdditives.UnloadScene("PauseMenu");
        onPauseMenuEscape();
    } 
}
