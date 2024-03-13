using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AI;

public class SongSelectionMenu : MonoBehaviour
{
    private List<SongContainer> songs = new();
    [HideInInspector] public static HighScoreKeeper highScores = new();
    public Button prefab;
    public TMP_Text description;
    public TMP_Text difficulty;
    public TMP_Text highScore;
    public static SongContainer selectedSong;
    private IDataPersistence jsonData = new JsonDataPersistence();

    AsyncOperationHandle<IList<TextAsset>> loadHandler;

    private void Start()
    {
        GetHighScores();
        StartCoroutine(InitializeSelectableSongs());
        selectedSong = null;
    }

    public void UpdateMetadata(SongContainer song)
    {
        Debug.Log(song.SongDescription);
        description.text = song.SongDescription;
        difficulty.text = song.SongDifficulty;
        highScore.text = highScores.GetHighScore(song.SongName).ToString();
        selectedSong = song;
        JSONRead.noteSpeedFactor = song.SongSpeed;
        AudioDelayer.offset = song.SongOffset;
    }

    public void PlaySong()
    {
        if(selectedSong != null)
            SceneManager.LoadSceneAsync("Main");
    }

    public void ReturnToMain()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public SongContainer LoadSongMetadata(TextAsset textAsset)
    {
        return jsonData.LoadData<SongContainer>(textAsset);
    }

    public IEnumerator InitializeSelectableSongs()
    {
        Debug.Log("Initializing");
        loadHandler = Addressables.LoadAssetsAsync<TextAsset>("metadata", null);
        yield return loadHandler;
        if (loadHandler.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Succeeded loading Addresses:");
            loadHandler.Completed += Jsongs =>
            {
                foreach (var song in Jsongs.Result)
                {
                    Debug.Log(song);
                    songs.Add(LoadSongMetadata(song));
                }
                foreach (var song in songs)
                {
                    Button newPrefab = Instantiate(prefab, transform);
                    newPrefab.transform.GetChild(0).GetComponent<TMP_Text>().text = song.SongName;
                    newPrefab.onClick.AddListener(() => UpdateMetadata(song));
                }
            };
        }
    }

    public void GetHighScores()
    {
        try
        {
            highScores = jsonData.LoadData<HighScoreKeeper>("/highscores.json");
        }
        catch
        {
            highScores = new();
        }
    }

    private void OnDestroy()
    {
        Addressables.Release(loadHandler);
    }
}
