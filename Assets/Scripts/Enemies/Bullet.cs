using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    [SerializeField] private string obstacleTag = "Obstacle";
    [SerializeField] private string playerTag = "Player";
    protected float _damage;
    protected Rigidbody2D _bulletRigidbody;

    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void StartProjectile(Transform target, float damage)
    {
        _damage = damage;
        _bulletRigidbody.transform.rotation = target.rotation;
        _bulletRigidbody.AddForce(target.up * speed, ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == obstacleTag)
        {
            Disable();
        }
        else if (other.tag == playerTag)
        {
            if (other.TryGetComponent(out Health health))
            {
                health.Damage(_damage);
            }

            Disable();
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        _bulletRigidbody.velocity = Vector3.zero;
    }

}
