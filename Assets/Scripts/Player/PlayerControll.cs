using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public bool lockMoviment;
    public int speed;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _moveCharacter;
    private Vector2 _moveVelocity;
    private bool died;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        GameManager.Instance.OnPlayerDie += PlayerDied;
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!lockMoviment)
            Move(Calculo());
    }

    public void SetLock(bool block)
    {
        if (died) return;
        lockMoviment = block;
        Move(Vector2.zero);
        _animator.SetTrigger("Die");
    }

    public void PlayerDied()
    {
        SetLock(true);
        died = true;
        _rigidbody2D.isKinematic = true;
    }

    private Vector2 Calculo()
    {
        float moveX = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");
        Mathf.Sin(moveX);
        _moveCharacter = new Vector2(moveX, movey);
        _animator.SetFloat("Horizontal", Mathf.Sin(moveX));
        return _moveCharacter;
    }

    public void Animator(string name)
    {
        _animator.SetTrigger(name);
    }
    public void Move(Vector2 _moveCharacter)
    {
        _rigidbody2D.velocity = new Vector2(_moveCharacter.x * speed, _moveCharacter.y * speed);
    }
}
