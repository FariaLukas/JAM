using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public int speed;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector3 _moveCharacter;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveCharacter = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0f);

        transform.position = transform.position + _moveCharacter * speed * Time.deltaTime;
      //  _rigidbody2D.AddForce(_moveCharacter * speed * Time.deltaTime);
    }
}
