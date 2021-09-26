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
    private Vector2 velocit;

    public GameObject armPosition;
    public float throwForce;
    public float timeToHold;
    public int keyboard;
    public float damage;
    public string enemies = "Enemy";
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2d = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {    
        if (transform.parent != null)
            _throwArm = true;

        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, _mousePos);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _mousePos);
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
        _rigidbody2D.isKinematic = false;
        transform.parent = null;
        velocit = new Vector2(_mousePos.x, _mousePos.y);

        transform.up = (Vector3)_mousePos - transform.position;
        _rigidbody2D.AddForce(transform.up * force);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.layer = 8;
        _timeHold = 0f;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.layer = 6;
            transform.position = new Vector2(0, 0);
            transform.rotation = collision.transform.rotation;
            transform.SetParent(armPosition.transform,false);
            Debug.Log(transform.position);
        }
        else if (collision.collider.tag == enemies &&
        collision.gameObject.TryGetComponent(out Health health))
        {
            health.Damage(damage);
        }

    }
}
