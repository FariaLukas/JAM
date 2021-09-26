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
    public bool canActivePowerUp;
    public Texture2D cursor;

    public Action OnUpdateScore;
    public Action OnPlayerDamage;
    public Action OnPlayerHeal;
    public Action OnPowerUp;
    public Action OnPowerDown;
    public Action OnPlayerDie;
    private float xspot, yspot;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        currentScore.value = 0;
        cursorSet(cursor);
    }

    void cursorSet(Texture2D tex)
    {
        CursorMode mode = CursorMode.ForceSoftware;
        xspot = tex.width / 2;
        yspot = tex.height / 2;
        Vector2 hotSpot = new Vector2(xspot, yspot);
        Cursor.SetCursor(tex, hotSpot, mode);
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
        canActivePowerUp = false;
        OnPowerUp?.Invoke();
    }

    public void PowerDown()
    {
        OnPowerDown?.Invoke();
    }

}
