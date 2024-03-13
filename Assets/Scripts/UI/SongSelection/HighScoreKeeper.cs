using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class HighScoreKeeper
{
    private List<Tuple<string, float>> _highScore = new();


    public List<Tuple<string, float>> HighScore
    {
        get { return _highScore; }
        set { _highScore = value; }
    }

    public float GetHighScore (string str)
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

    public void SetHighScore (string str, float i)
    {
        foreach (var tuple in HighScore)
        {
            if(tuple.Item1 == str)
            {
                HighScore.Remove(tuple);
            }
        }
        HighScore.Add(new Tuple<string, float>(str, i));
    }

}
