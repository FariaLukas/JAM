using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public int speed;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _moveCharacter;
    private Vector2 _moveVelocity;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        Calculo();
    }
    
    private void Calculo()
    {
        float moveX = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");

        _moveCharacter = new Vector2(moveX, movey);
    }
    private void Move()
    {
        _rigidbody2D.velocity = new Vector2(_moveCharacter.x * speed, _moveCharacter.y * speed);
    }
}
