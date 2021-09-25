using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public int speed;
    private CharacterController _characterController;
    private Animator _animator;
    private Vector2 _moveCharacter;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _moveCharacter = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        _characterController.Move(_moveCharacter * speed * Time.deltaTime);
    }
}
