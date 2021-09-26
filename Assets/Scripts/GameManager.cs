using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject addScorePFB;
    public GlobalInt currentScore;
    public GlobalInt playerLife;

    public Action OnUpdateScore;
    public Action OnPlayerDamage;
    public Action OnPlayerHeal;
    public Action OnPowerUp;
    public Action OnPowerDown;
    public Action OnPlayerDie;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        currentScore.value = 0;

    }

    public void AddScore(int score, Vector3 position)
    {
        currentScore.value += score;
        GameObject go = Instantiate(addScorePFB, position, Quaternion.identity);
        TextMeshProUGUI t = go.GetComponentInChildren<TextMeshProUGUI>();
        t.text = "+" + score;
        Destroy(go, 0.7f);
        OnUpdateScore?.Invoke();

        Save.Instance.SaveScore(currentScore.value);
    }

    public void PlayerDamage()
    {
        OnPlayerDamage?.Invoke();

        PlayerDie();

    }
    public void AddLife()
    {
        OnPlayerHeal?.Invoke();

    }

    public void PlayerDie()
    {

        if (playerLife.value <= 0)
        {
            OnPlayerDie?.Invoke();
        }
    }

    public void PowerUp()
    {
        OnPowerUp?.Invoke();
    }

    public void PowerDown()
    {
        OnPowerDown?.Invoke();
    }

}
