
using System;
using System.ComponentModel.Design;
/// <summary>
/// SettingsContainer is the container class object to hold all the settings data within the game.
/// It is meant to be serialized with JsonDataPersistence, so any changes to this container will persist through runtimes.
/// Every variable in this class intefaces with the SettingsMenu.
/// </summary>
[System.Serializable]
public class SettingsContainer
{
    [Newtonsoft.Json.JsonIgnore] public System.Action onResolutionChange;
    [Newtonsoft.Json.JsonIgnore] public System.Action onWindowTypeChange;
    [Newtonsoft.Json.JsonIgnore] public System.Action onAutosaveChange;


    private float _masterVolume;
    private float _bgmVolume;
    private float _sfxVolume;
    private string _resolution;
    private string _windowType;
    private bool _autosaveSettings;

    public float MasterVolume
    {
        get { return _masterVolume; }
        set { _masterVolume = value; }
    }
    public float BGMVolume
    {
        get { return _bgmVolume; }
        set { _bgmVolume = value; }
    }
    public float SFXVolume
    {
        get { return _sfxVolume; }
        set { _sfxVolume = value; }
    }

    public string Resolution
    {
        get { return _resolution; }
        set { _resolution = value; onResolutionChange?.Invoke(); }
    }
    public string WindowType
    {
        get { return _windowType; }
        set { _windowType = value; onWindowTypeChange?.Invoke(); }
    }

    public bool AutosaveSettings
    {
        get { return _autosaveSettings; }
        set { _autosaveSettings = value; onAutosaveChange?.Invoke(); }
    }

    public SettingsContainer()
    {
        MasterVolume = 0;
        BGMVolume = 0;
        SFXVolume = 0;
        Resolution = "2560x1440";
        WindowType = "Windowed";
        AutosaveSettings = true;
    }
}