using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour, IInitializable
{
    public TextMeshProUGUI highScoreText;
    public string scoreLabel = "HI-SCORE ";
    public bool showOnEnable = true;
    public bool inMenu;

    private void OnEnable()
    {
        if (showOnEnable)
            ShowHighScore();
    }

    public void Init()
    {
      
    }

    public void ShowHighScore()
    {
        highScoreText.text = scoreLabel + Save.Instance.highScoreValue;
    }

    public void UpdateHighScore(int Value)
    {
        highScoreText.text = scoreLabel + Value;
    }

}
