using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static Save Instance;
    public string highScoreKey;
    public int highScoreValue { get; set; }
    public int oldHighScore { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(this);
        }

        highScoreValue = oldHighScore = Load(highScoreKey);

    }

    public void SaveScore(int newScore)
    {
        if (newScore > Load(highScoreKey))
        {
            highScoreValue = newScore;
            PlayerPrefs.SetInt(highScoreKey, highScoreValue);
        }
    }

    public int Load(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
}
