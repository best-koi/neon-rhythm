using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAdditives : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadScene("Main Menu", LoadSceneMode.Additive);
    }

    public static void LoadScene(string scene, LoadSceneMode n)
    {
        SceneManager.LoadSceneAsync(scene, n);
    }

    public static void UnloadScene(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }
}
