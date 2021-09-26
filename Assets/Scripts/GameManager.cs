using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GlobalInt currentScore;
    public GameObject addScorePFB;
    public Action OnUpdateScore;
    public Action OnPlayerDamage;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    public void AddScore(int score, Vector3 position)
    {
        currentScore.value += score;
        GameObject go = Instantiate(addScorePFB, position, Quaternion.identity);
        TextMeshProUGUI t = go.GetComponentInChildren<TextMeshProUGUI>();
        t.text = "+" + score;
        Destroy(go, 0.7f);
        OnUpdateScore?.Invoke();
    }

    public void PlayerDamage()
    {
        OnPlayerDamage?.Invoke();
    }



}
