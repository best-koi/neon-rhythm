using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void Play() {
        SceneAdditives.LoadScene("Main", LoadSceneMode.Single); 
    }//end Play Button


    public void Options()
    {
        SceneAdditives.LoadScene("OptionsMenu", LoadSceneMode.Additive);
        SceneAdditives.UnloadScene("Main Menu");
    }

    public void MeetTheStagehands()
    {
        //TODO: Make a Scene transition if needed, otherwise just swap out the UI later
    }

    public void Quit()
    {
        Application.Quit();
    }//end Quit Button
}
