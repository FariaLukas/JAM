using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Health : MonoBehaviour
{
    public float currentLife;
    public float invencibilityTime;

    [Header("DeathAnimation")]
    public Color deathColor = Color.red;
    public float duration = 0.2f;
    public int deathFeedback = 4;
    public Action OnKill;

    [Header("Invencibility")]
    public Color hitColor = Color.cyan;
    public int hitTime = 4;

    private Collider2D _collider;
    private SpriteRenderer _render;
    private Color _inicialColor;
    private bool _canTakeDamage = true;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _render = GetComponent<SpriteRenderer>();
        _inicialColor = _render.color;
        if (invencibilityTime == 0)
            hitColor = _inicialColor;
    }

    public void Initialize()
    {
        _render.color = _inicialColor;
        _collider.enabled = true;
    }

    public void SetInitialLife(float life)
    {
        currentLife = life;
    }

    public virtual void Damage(float damage)
    {
        if (!_canTakeDamage) return;

        currentLife -= damage;

        _canTakeDamage = false;

        if (currentLife <= 0)
        {
            Kill();
            return;
        }

        StartCoroutine(WaitToTakeDamage());
    }

    public virtual void Kill()
    {
        OnKill?.Invoke();
        _collider.enabled = false;

        BlinkAnimation(deathColor, duration, deathFeedback).OnComplete(() => Killling());

    }
    private void Killling()
    {
        print("AAAAA");
        gameObject.SetActive(false);
    }

    private Tween BlinkAnimation(Color endValue, float duration, int loop)
    {
        float timer = (float)duration / loop * 2;

        _render.DOKill();
        _render.color = _inicialColor;
        return _render.DOColor(endValue, timer).SetLoops(loop * 2, LoopType.Yoyo);
    }

    IEnumerator WaitToTakeDamage()
    {

        yield return BlinkAnimation(hitColor, invencibilityTime, hitTime).WaitForCompletion();

        _canTakeDamage = true;
    }
}
