using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongEnding : MonoBehaviour
{
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = $"YOU GOT {JSONRead.finalScore} POINTS";
    }

    public void BackToMain()
    {
        SceneAdditives.LoadScene("SceneManagement", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
