using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PowerUp : MonoBehaviour
{
    public Image displayer;
    public GlobalInt currentPoints;
    public float maxPower = 400f;

    [Header("Animation")]
    public float fillDuration = .2f;
    public float releaseDuration = 3f;
    public Ease ease = Ease.InExpo;

    public GameObject feedack;

    private float _currentPower;
    private float _oldPoints;
    private bool _block;

    private void Start()
    {
        GameManager.Instance.OnUpdateScore += UpdateUI;
        GameManager.Instance.OnPowerUp += ResetPoints;
        GameManager.Instance.OnPowerDown += ActivePowerCount;

        _oldPoints = currentPoints.value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _currentPower += currentPoints.value - _oldPoints;

        _oldPoints = currentPoints.value;

        if (_block) return;

        UpdateDisplay(fillDuration);

        if (_currentPower >= maxPower)
        {
            GameManager.Instance.canActivePowerUp = true;
            feedack.SetActive(true);
            _block = true;
        }

    }

    private void UpdateDisplay(float duration)
    {
        float amount = _currentPower / maxPower;

        displayer.DOFillAmount(amount, duration).SetEase(ease);
    }

    private void ResetPoints()
    {
        _currentPower = 0;
        UpdateDisplay(releaseDuration);
    }

    private void ActivePowerCount()
    {
        _block = false;

        _currentPower = 0;


    }

    [Button]
    public void Add(float a)
    {

        float amount = a / maxPower;
        displayer.DOFillAmount(amount, fillDuration).SetEase(ease);

    }
}
