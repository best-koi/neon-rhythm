using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class HighScoreKeeper
{
    private List<Tuple<string, int>> _highScore = new();


    public List<Tuple<string, int>> HighScore
    {
        get { return _highScore; }
        set { _highScore = value; }
    }

    public int GetHighScore (string str)
    {
        foreach(var tuple in HighScore)
        {
            if(tuple.Item1 == str)
            {
                return tuple.Item2;
            }
        }
        return 0;
    }

    public void SetHighScore (string str, int i)
    {
        foreach (var tuple in HighScore)
        {
            if(tuple.Item1 == str)
            {
                HighScore.Remove(tuple);
            }
        }
        HighScore.Add(new Tuple<string, int>(str, i));
    }
}
