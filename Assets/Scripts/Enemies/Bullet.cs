using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string obstacleTag = "Obstacle";
    [SerializeField] private string playerTag = "Player";
    private float _damage;
    private Rigidbody2D _bulletRigidbody;

    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    public void StartProjectile(Transform target, float damage, float offset)
    {
        _damage = damage;

        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - offset;
        _bulletRigidbody.rotation = angle;

        _bulletRigidbody.AddForce(direction.normalized * speed, ForceMode2D.Impulse);

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
