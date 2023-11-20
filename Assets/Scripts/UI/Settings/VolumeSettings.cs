using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    //Volume Data
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Text masterVolumeValueText;
    [SerializeField] private TMP_Text bgmVolumeValueText;
    [SerializeField] private TMP_Text sfxVolumeValueText;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    private SettingsContainer settingsData;

    public float MasterVolume
    {
        get { return masterVolumeSlider.value; }
    }

    public float BGMVolume
    {
        get { return bgmVolumeSlider.value; }
    }

    public float SFXVolume
    {
        get { return sfxVolumeSlider.value; }
    }

    private void Start()
    {
        UpdateMasterVolumeOnChange(masterVolumeSlider.value);
        UpdateBgmVolumeOnChange(bgmVolumeSlider.value);
        UpdateSfxVolumeOnChange(sfxVolumeSlider.value);

        masterVolumeSlider.onValueChanged.AddListener(delegate {
            UpdateMasterVolumeOnChange(masterVolumeSlider.value); });
        bgmVolumeSlider.onValueChanged.AddListener(delegate {
            UpdateBgmVolumeOnChange(bgmVolumeSlider.value); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate {
            UpdateSfxVolumeOnChange(sfxVolumeSlider.value); });

    }

    public void Initialize(SettingsContainer settings)
    {
        settingsData = settings;
        masterVolumeSlider.value = settingsData.MasterVolume;
        bgmVolumeSlider.value = settingsData.BGMVolume;
        sfxVolumeSlider.value = settingsData.SFXVolume;
    }

    public void UpdateMasterVolumeOnChange(float value)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log(value) * 20f);
        masterVolumeValueText.text = Mathf.Round(value * 100).ToString() + "%";
        settingsData.MasterVolume = value;
    }

    public void UpdateBgmVolumeOnChange(float value)
    {
        audioMixer.SetFloat("bgmVolume", Mathf.Log(value) * 20f);
        bgmVolumeValueText.text = Mathf.Round(value * 100).ToString() + "%";
        settingsData.BGMVolume = value;
    }

    public void UpdateSfxVolumeOnChange(float value)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log(value) * 20f);
        sfxVolumeValueText.text = Mathf.Round(value * 100).ToString() + "%";
        settingsData.SFXVolume = value;
    }
}
