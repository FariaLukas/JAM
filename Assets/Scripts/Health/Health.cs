using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Sirenix.OdinInspector;

public class Health : MonoBehaviour
{
    public float currentLife { get; set; }

    [Header("DeathAnimation")]
    public string deathParameter = "Die";
    public Color deathColor = Color.red;
    public float duration = 0.2f;
    public int deathFeedback = 4;
    public Action OnKill;

    [Header("Invencibility")]
    public float invencibilityTime;
    [ShowIf(nameof(HasInvencibility))]
    public Color hitColor = Color.cyan;
    [ShowIf(nameof(HasInvencibility))]
    public int hitTime = 4;
    public bool canPrint;
    private Collider2D _collider;
    private SpriteRenderer _render;
    private Color _inicialColor;
    private bool _canTakeDamage = true;
    private Animator _animator;

    private bool HasInvencibility()
    {
        return invencibilityTime > 0;
    }


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _render = GetComponent<SpriteRenderer>();
        _inicialColor = _render.color;
        if (invencibilityTime == 0)
            hitColor = _inicialColor;
        _animator = GetComponent<Animator>();
    }


    public void Initialize()
    {
        _render.color = _inicialColor;
        _collider.enabled = true;
    }

    public void SetInitialLife(float life)
    {
        currentLife = life;
        _canTakeDamage = true;
    }

    public virtual void Damage(float damage, GameObject g)
    {
        if (!_canTakeDamage) return;

        _canTakeDamage = false;

        currentLife -= damage;

        if (currentLife <= 0)
        {
            Kill();
            return;
        }

        StartCoroutine(WaitToTakeDamage());
    }

    [Button]
    public virtual void Kill()
    {
        OnKill?.Invoke();
        _collider.enabled = false;

        _animator.SetTrigger(deathParameter);
        //BlinkAnimation(deathColor, duration, deathFeedback).OnComplete(() => Killling());

    }
    private void Killling()
    {

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
