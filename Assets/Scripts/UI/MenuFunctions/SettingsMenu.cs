using UnityEngine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    private SettingsContainer settingsData = new();
    private IDataPersistence jsonData = new JsonDataPersistence();
    [SerializeField] private VolumeSettings volumeSettings;

    private void Awake()
    {
        LoadSettingsData();
        volumeSettings.Initialize(settingsData);
    }

    private void OnApplicationQuit()
    {
        if(settingsData.AutosaveSettings)
        {
            SaveSettingsData();
        }
    }

    public void MainMenu()
    {
        SceneAdditives.LoadScene("Main Menu", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        SceneAdditives.UnloadScene("OptionsMenu");
    }

    
    public void EnablerDisabler(GameObject obj)
    {
        bool objActivity = obj.activeInHierarchy ? false : true;
        obj.SetActive(objActivity);

    }

    public void TextHighlighter(TMP_Text textToHighlight)
    {
        textToHighlight.color = (textToHighlight.color == Color.yellow) ? Color.white : Color.yellow;
        textToHighlight.fontStyle = (textToHighlight.fontStyle == FontStyles.Underline) ? FontStyles.Normal : FontStyles.Underline;
    }
 
    public void SaveSettingsData()
    {
        jsonData.SaveData<SettingsContainer>("/settings.json", settingsData);
    }

    public void DeleteSettingData()
    {
        jsonData.DeleteData("/settings.json");
        jsonData.DeleteData("/settings.json.tmp");
    }

    private void LoadSettingsData()
    {
        //If there is no settings to load (FileNotFound exception), go with the default!
        try
        {
            settingsData = jsonData.LoadData<SettingsContainer>("/settings.json");
        } catch
        {
            settingsData = new();
        }
    }
}
