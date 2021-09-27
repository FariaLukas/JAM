using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerArms : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _mousePos;
    private BoxCollider2D _collider2d;
    private bool _throwArm;
    private float _timeHold;
    private Vector2 _velocit;
    private bool _enableToDamage;

    public PlayerControll playerControll;
    public GameObject armPosition;
    public GameObject feedback;
    public ArmFeedback armFeedback;
    public float throwForce;
    public float timeToHold;
    public int keyboard;
    public AUDIO catchARM;

    [Header("Damgae")]
    public float damage;
    public string enemies = "Enemy";

    [Header("Animation")]
    private Animator _animator;
    public float duration = .3f;
    public Ease ease = Ease.OutBack;

    [Header("Collect")]
    public float timeToCatch = 0.3f;
    private AUDIO aUDIO;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2d = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        GameManager.Instance.OnPlayerDie += PlayerDeath;
        aUDIO = GetComponent<AUDIO>();
    }

    private void PlayerDeath()
    {
        gameObject.SetActive(false);
        armFeedback.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (transform.parent == null)
        {
            return;
        }
        else
        {
            _throwArm = true;
            float amout = Calculate(_timeHold) / throwForce;
            armFeedback.UpdateFill(amout);
        }

        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, _mousePos);

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

        playerControll.Animator("Attack");
        _animator.SetBool("Attack", true);
        _rigidbody2D.isKinematic = false;
        transform.parent = null;
        _velocit = new Vector2(_mousePos.x, _mousePos.y);

        transform.up = (Vector3)_mousePos - transform.position;
        _rigidbody2D.AddForce(transform.up * force);

        _enableToDamage = true;
        armFeedback.gameObject.SetActive(false);
        feedback.SetActive(true);
        Invoke(nameof(EnableToCatch), timeToCatch);
        aUDIO.PLAy();
    }

    private void EnableToCatch()
    {
        gameObject.layer = 8;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetBool("Attack", false);
        _timeHold = 0f;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;

        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.layer = 6;

            transform.position = new Vector2(0, 0);
            transform.rotation = collision.transform.rotation;
            transform.SetParent(armPosition.transform, false);
            feedback.SetActive(false);
            _enableToDamage = true;
            armFeedback.gameObject.SetActive(true);
            catchARM.PLAy();

        }
        else if (collision.collider.tag == enemies &&
        collision.gameObject.TryGetComponent(out Health health))
        {
            if (_enableToDamage)
            {
                health.Damage(damage, gameObject);
                _enableToDamage = false;
            }

        }

    }

}
