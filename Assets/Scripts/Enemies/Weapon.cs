using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float ratio;
    public float offset;
    private Pool _pool;
    private float _delay;

    private void Awake()
    {
        _pool = GetComponent<Pool>();

    }

    public void OverrideRatio(float r)
    {
        ratio = r;
    }

    public void Shoot(Transform target, float damage)
    {
        if (_delay <= 0)
        {
            _delay = ratio;

            GameObject bullet = _pool.GetPooledGameObject();
            bullet.transform.position = transform.position;
            bullet.SetActive(true);

            if (bullet.TryGetComponent(out Bullet b))
            {
                b.StartProjectile(target, damage, offset);
            }


        }
        else
        {
            _delay -= Time.deltaTime;
        }
    }
}
