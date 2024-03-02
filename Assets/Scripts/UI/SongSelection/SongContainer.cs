using UnityEngine;

[System.Serializable]
public class SongContainer
{
    private string _songName;
    private string _songDescription;
    private string _songDifficulty;

    public string SongName
    {
        get { return _songName; }
        set { _songName = value; }
    }

    public string SongDescription
    {
        get { return _songDescription; }
        set { _songDescription = value; }
    }

    public string SongDifficulty
    {
        get { return _songDifficulty; }
        set { _songDifficulty = value; }
    }
}
