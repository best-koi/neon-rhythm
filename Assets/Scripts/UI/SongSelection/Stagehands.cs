using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stagehands : MonoBehaviour
{

    public void MainMenu()
    {
        SceneAdditives.LoadScene("Main Menu", LoadSceneMode.Additive);
        SceneAdditives.UnloadScene("Meet The Stagehands");
    }
}
