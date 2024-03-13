using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneAdditives : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        LoadScene("Main Menu", LoadSceneMode.Additive);
    }
    public static void LoadScene(string str, LoadSceneMode n)
    {
        SceneManager.LoadScene(str, n);
    }

    public static void UnloadScene(string str)
    {
        SceneManager.UnloadSceneAsync(str);
        
    }
    
}
