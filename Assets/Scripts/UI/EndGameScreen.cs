using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EndGameScreen : MonoBehaviour, IInitializable
{
    public float timeToActive = 0.5f;
    public float scoreDelay = 0.5f;
    public float highScoreDuration = 1;
    public TextMeshProUGUI scoreText;
    public string label;
    public float counterDuration = 0.5f;

    public TextMeshProEffect newHighScore;
    public GlobalInt currentScore;
    public UnityEvent OnFinish;
    public UnityEvent OnEnableObject;
    private HighScoreDisplay highScore;

    public void Init()
    {
        highScore = GetComponent<HighScoreDisplay>();
        GameManager.Instance.OnPlayerDie += ScreenActivation;
    }

    private void ScreenActivation()
    {
        gameObject.SetActive(true);
        OnEnableObject?.Invoke();
        scoreText.text = "";
        highScore.UpdateHighScore(Save.Instance.oldHighScore);
        Invoke(nameof(DelayScore), scoreDelay);
    }

    private void DelayScore()
    {

        scoreText.DOCounter(0, currentScore.value, counterDuration).OnComplete(() => UpdateHighScore());

    }

    private void UpdateHighScore()
    {
        int current = Save.Instance.highScoreValue;
        highScore.highScoreText.DOText(highScore.scoreLabel + current, highScoreDuration).OnComplete(() => FinishCount());

    }

    private void FinishCount()
    {
        OnFinish?.Invoke();
        if (BeatHighScore())
            ShowNewHighScore();
    }

    private void ShowNewHighScore()
    {
        newHighScore.enabled = BeatHighScore();
        highScore.UpdateHighScore(Save.Instance.highScoreValue);
    }

    private bool BeatHighScore()
    {
        return Save.Instance.highScoreValue > Save.Instance.oldHighScore;
    }

    private void DelayedScreenActivation()
    {
        Invoke(nameof(ScreenActivation), timeToActive);
    }
}
