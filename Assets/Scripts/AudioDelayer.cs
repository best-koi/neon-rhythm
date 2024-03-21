using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioDelayer : MonoBehaviour
{
    private AudioSource m_Source;
    private JSONRead inputJson;
    private float yieldSeconds;
    bool paused;
    public static float offset;

    //Used for Asyncronously finding the song chosen using Addressables
    AsyncOperationHandle<AudioClip> audioHandler;

    private IEnumerator Start()
    {
        PauseMenu.onPauseMenuEscape += ResumeSong;
        paused = false;
        inputJson = FindObjectOfType<JSONRead>();
        yieldSeconds = JSONRead.noteSpeedFactor - inputJson.goodTimeLeeway + offset;
        m_Source = this.GetComponent<AudioSource>();
        yield return StartCoroutine(InitializeSelectedSong());
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

    private void ResumeSong()
    {
        Time.timeScale = 1;
        m_Source.Play();
        paused = false;
    }

    public IEnumerator InitializeSelectedSong()
    {
        audioHandler = Addressables.LoadAssetAsync<AudioClip>(SongSelectionMenu.selectedSong.SongName + " Song");
        yield return audioHandler;
        if (audioHandler.Status == AsyncOperationStatus.Succeeded)
        {
            m_Source.clip = audioHandler.Result;
            m_Source.PlayDelayed(yieldSeconds);
        }
    }

    private void OnDestroy()
    {
        Addressables.Release(audioHandler);
        Destroy(this);
    }
}
