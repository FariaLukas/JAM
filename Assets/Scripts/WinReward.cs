using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WinReward : MonoBehaviour
{
    public List<Rewards> rewards = new List<Rewards>();
    public TextMeshProUGUI text;
    public float duration;

    private int _currentScore;

    public void CheckForHighScore()
    {

        foreach (var r in rewards)
        {
            if (Save.Instance.highScoreValue >= r.scoreToShow)
                _currentScore = r.scoreToShow;
        }

        foreach (var r in rewards)
        {
            if (r.scoreToShow == _currentScore)
                text.DOText(r.text, duration);
        }
    }


}

[System.Serializable]
public class Rewards
{
    [TextArea]
    public string text;
    public int scoreToShow;
}
