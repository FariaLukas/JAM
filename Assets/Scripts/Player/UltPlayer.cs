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
    public float cameraDuration = 3, cameraStrenght = 3;
    public int cameraVirato = 10;

    [Header("Damgae")]
    public float damage = 1;
    public string enemies = "Enemy";

    private Rigidbody2D _rigidbody2D;
    private Vector2 guardar;
    private bool _active = false;
    private SpriteRenderer _spriteRenderer;
    public Sprite _sprite;
    private bool _canActive;
    private Health _pHealth;
    private Camera _mainCamera;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        GameManager.Instance.OnPowerUp += Active;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pHealth = playerControll.GetComponent<Health>();
        GameManager.Instance.OnPlayerDie += Deactive;
        _mainCamera = FindObjectOfType<Camera>();
    }
    private void Update()
    {

        if (Input.GetKeyDown(keyCode) && GameManager.Instance.canActivePowerUp)
        {
            if (_active) return;
            GameManager.Instance.PowerUp();
            _spriteRenderer.sprite = _sprite;
            playerControll._animator.SetBool("Ult", true);
            _active = true;
            _canActive = false;
            guardar = transform.position;

            StartCoroutine(Return(_active));
            playerControll.lockMoviment = true;
            playerControll.Move(Vector2.zero);
            AddForce();
            _pHealth.SetDamagable(false);
        }
    }

    private void Active()
    {
        _canActive = true;

    }
    private void Deactive()
    {
        _active = false;
        StopAllCoroutines();
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

        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMove(guardar, duration).SetEase(ease));
        s.Join(transform.DORotate(Vector3.zero, duration)).OnComplete(() => Return());
        _rigidbody2D.angularVelocity = 0f;
        playerControll.lockMoviment = false;
        _active = false;
        _pHealth.ResetInvencible();
        GameManager.Instance.PowerDown();
    }

    private IEnumerator Return(bool active)
    {
        yield return new WaitForSeconds(3f);
        Test();
    }

    private void Return()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _spriteRenderer.sprite = null;
        playerControll._animator.SetBool("Ult", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == enemies &&
           collision.gameObject.TryGetComponent(out Health health))
        {
            health.Damage(damage, gameObject);

        }

    }
}
