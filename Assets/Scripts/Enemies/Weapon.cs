using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float ratio;
    public List<Transform> muzzles;
    protected Pool _pool;
    protected float _delay;
    private Transform _muzzleParent;

    private void Awake()
    {
        _pool = GetComponent<Pool>();
        _muzzleParent = muzzles[0].parent;
    }

    public void OverrideRatio(float r)
    {
        ratio = r;
    }

    public virtual void Shoot(Transform target, float damage, Animator anim, string trigger)
    {
        if (_delay <= 0)
        {
            _delay = ratio;

            anim.SetTrigger(trigger);

            _muzzleParent.transform.up = target.position - transform.position;

            foreach (var m in muzzles)
            {
                GameObject bullet = _pool.GetPooledGameObject();
                bullet.transform.position = _muzzleParent.position;
                bullet.SetActive(true);

                if (bullet.TryGetComponent(out Bullet b))
                {
                    b.StartProjectile(m, damage);
                }
            }

        }
        else
        {
            _delay -= Time.deltaTime;
        }
    }
}
