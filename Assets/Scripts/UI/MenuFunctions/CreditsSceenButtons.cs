using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScreenButtons : MonoBehaviour
{
    public void ReturnToMain() {
        SceneAdditives.LoadScene("Main Menu", LoadSceneMode.Single); 
    }
}
