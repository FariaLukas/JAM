using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UltPlayer : MonoBehaviour
{
    [SerializeField] private Vector2 xRandom, yRandom;
    public PlayerControll playerControll;
    public KeyCode keyCode;
    public float ThrowForce;

    [Header("Animation")]
    public float duration = .5f;
    public Ease ease = Ease.InBack;

    private Rigidbody2D _rigidbody2D;
    private Vector2 guardar;
    private bool _active = false;
    private SpriteRenderer _spriteRenderer;
    public Sprite _sprite;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        GameManager.Instance.OnPowerUp += Active;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Active()
    {
        if (_active) return;
        _spriteRenderer.sprite = _sprite; 
        playerControll._animator.SetBool("Ult", true);
        _active = true;
        guardar = transform.position;
        StartCoroutine(Return(_active));
        playerControll.lockMoviment = true;
        playerControll.Move(Vector2.zero);
        AddForce();

    }

    private void AddForce()
    {
        float x = Random.Range(xRandom.x, xRandom.y);
        float y = Random.Range(yRandom.x, yRandom.y);
        Vector2 force = new Vector2(x, y);
        transform.up = force;

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(transform.up * ThrowForce);
    }

    private void Test()
    {
        _spriteRenderer.sprite = null;
        playerControll._animator.SetBool("Ult", false);
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMove(guardar, duration).SetEase(ease));
        s.Join(transform.DORotate(Vector3.zero, duration)).OnComplete(() => transform.rotation = Quaternion.Euler(0, 0, 0));
        _rigidbody2D.angularVelocity = 0f;
        playerControll.lockMoviment = false;
        _active = false;
        GameManager.Instance.PowerDown();
    }

    private IEnumerator Return(bool active)
    {
        yield return new WaitForSeconds(3f);
        Test();
    }
}
