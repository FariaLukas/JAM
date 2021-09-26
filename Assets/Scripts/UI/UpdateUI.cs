using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UpdateUI : MonoBehaviour
{
    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public GlobalInt currentScore;
    public float textDuration = .5f;
    [Header("Life")]
    public TextMeshProUGUI currentLife;
    public Vector3 punchForce = new Vector3(0, .5f, 0);
    public float duration = .2f;

    private Health _playerHealth;

    private int oldScore;
    private float oldLife;

    private void Start()
    {
        GameManager.Instance.OnUpdateScore += UpdateDisplay;
        GameManager.Instance.OnPlayerDamage += UpdateLife;

        _playerHealth = FindObjectOfType<PlayerControll>().GetComponent<Health>();

        scoreText.text = currentScore.value.ToString();
        currentLife.text = "x" + _playerHealth.currentLife.ToString();
        oldLife = _playerHealth.currentLife;
    }

    private void UpdateLife()
    {
        if (oldLife == _playerHealth.currentLife) return;

        currentLife.text = "x" + _playerHealth.currentLife.ToString();
        oldLife = _playerHealth.currentLife;
        currentLife.transform.DOPunchScale(punchForce, duration).OnComplete(() => currentLife.transform.localScale = Vector3.one);
    }

    private void UpdateDisplay()
    {

        scoreText.DOCounter(oldScore, currentScore.value, textDuration);
        oldScore = currentScore.value;
        //scoreText.transform.DOPunchScale()
    }

}
