using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _mousePos;
    private BoxCollider2D _collider2d;
    private bool _throwArm;
    private float _timeHold;

    public GameObject armPosition;
    public float throwForce;
    public float timeToHold = 2f;
    public int keyboard;
    public float damage = 1;

    [Header("Tags")]
    public string enemies = "Enemy";

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2d = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (transform.parent != null)
            _throwArm = true;


        if (Input.GetMouseButton(keyboard))
        {
            _throwArm = false;
            _timeHold += Time.deltaTime;

        }
        if (Input.GetMouseButtonUp(keyboard))
        {
            Throw(Calculate(_timeHold));
        }
    }

    private float Calculate(float time)
    {
        float holdTime = Mathf.Clamp01(time / timeToHold);
        float force = holdTime * throwForce;
        return force;

    }

    void Throw(float force)
    {
        if (!_throwArm)
            return;

        transform.parent = null;
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(_mousePos * force);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidbody2D.isKinematic = true;
        gameObject.layer = 8;
        _rigidbody2D.velocity = new Vector2(0f, 0f);
        _timeHold = 0f;
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.layer = 6;
            transform.SetParent(armPosition.transform);
            transform.position = armPosition.transform.position;
        }
        else if (collision.collider.tag == enemies &&
         collision.gameObject.TryGetComponent(out Health health))
        {
            health.Damage(damage);
        }


    }
}
