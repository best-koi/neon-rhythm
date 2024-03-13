using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public string PlayScene;
    private SettingsContainer settingsData = new();
    private IDataPersistence jsonData = new JsonDataPersistence();
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        LoadSettingsData();
        audioMixer.SetFloat("masterVolume", Mathf.Log(settingsData.MasterVolume) * 20f);
        audioMixer.SetFloat("bgmVolume", Mathf.Log(settingsData.BGMVolume) * 20f);
        audioMixer.SetFloat("sfxVolume", Mathf.Log(settingsData.SFXVolume) * 20f);
    }

    public void Play() {
        SceneAdditives.LoadScene(PlayScene, LoadSceneMode.Additive);
        SceneAdditives.UnloadScene("Main Menu");
    }//end Play Button


    public void Options()
    {
        SceneAdditives.LoadScene("OptionsMenu", LoadSceneMode.Additive);
        SceneAdditives.UnloadScene("Main Menu");
    }

    public void MeetTheStagehands()
    {
        SceneAdditives.LoadScene("Credits Screen", LoadSceneMode.Additive);
        SceneAdditives.UnloadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }//end Quit Button

    private void LoadSettingsData()
    {
        //If there is no settings to load (FileNotFound exception), go with the default!
        try
        {
            settingsData = jsonData.LoadData<SettingsContainer>("/settings.json");
        }
        catch
        {
            settingsData = new();
        }
    }
}
