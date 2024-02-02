using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioDelayer : MonoBehaviour
{
    private AudioSource m_Source;
    private JSONRead inputJson;
    private float yieldSeconds;
    bool paused;
    public float offset;

    private void Start()
    {
        PauseMenu.onPauseMenuEscape += ResumeSong;
        paused = false;
        inputJson = FindObjectOfType<JSONRead>();
        yieldSeconds = inputJson.noteSpeedFactor - inputJson.successTimeLeeway + offset;
        m_Source = this.GetComponent<AudioSource>();
        m_Source?.Pause();
        StartCoroutine(DelaySong());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            Time.timeScale = 0;
            m_Source?.Pause();
            SceneAdditives.LoadScene("PauseMenu", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
        yieldSeconds -= Time.deltaTime;
    }

    IEnumerator DelaySong()
    {
        paused = true;
        yield return new WaitForSeconds(yieldSeconds);
        m_Source?.Play();
        paused = false;
    }

    private void ResumeSong()
    {
        Time.timeScale = 1;
        m_Source?.Play();
        paused = false;
    }
}
