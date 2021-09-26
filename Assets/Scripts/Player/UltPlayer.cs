using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltPlayer : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 headforce;
    public PlayerControll playerControll;
    public KeyCode keyCode;
    public float ThrowForce;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {   
        if (Input.GetKeyDown(keyCode))
        {
            PlayerControll.lockMoviment = true;
            playerControll.Move(Vector2.zero);
            transform.up = (Vector3)headforce - transform.position;
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.AddForce(transform.up * ThrowForce);
        }
    }
}
