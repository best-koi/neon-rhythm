using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    void Play() {
        //SceneManager.LoadScene(); 
        //TODO: replace with either song menu or actual game scene later
    }//end Play Button

    void MeetTheStagehands()
    {
        //TODO: Make a Scene transition if needed, otherwise just swap out the UI later
    }

    void Quit()
    {
        Application.Quit();
    }//end Quit Button
}
