using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltPlayer : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 headforce;
    private Vector2 guardar;
    private bool _active = false;
    public PlayerControll playerControll;
    public KeyCode keyCode;
    public float ThrowForce;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {  
        if (Input.GetKeyDown(keyCode) && !_active)
        {
            _active = true;
            guardar = transform.position;
            StartCoroutine(Return(_active));
            PlayerControll.lockMoviment = true;
            playerControll.Move(Vector2.zero);
            transform.up = (Vector3)headforce - transform.position;
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.AddForce(transform.up * ThrowForce);
        }
    }

    private void Test()
    {
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        transform.position = guardar;
        transform.rotation = Quaternion.Euler(0,0,0);
        PlayerControll.lockMoviment = false;
        _active = false;
    }

    private IEnumerator Return(bool active)
    {
        yield return new WaitForSeconds(3f);
        Test();
    }
}
