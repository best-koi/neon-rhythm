using UnityEngine;

[System.Serializable]
public class SongContainer
{
    private string _songName;
    private string _songDescription;
    private string _songDifficulty;
    private float _songSpeed;
    private float _songOffset;

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

    public float SongSpeed
    {
        get { return _songSpeed; }
        set { _songSpeed = value; }
    }

    public float SongOffset
    {
        get { return _songOffset; }
        set { _songOffset = value; }
    }
}
