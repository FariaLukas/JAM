using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCollectable : Collectable
{
    public GameObject prefab;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                if (health.currentLife == health.initialLife) return;
                ChangeCollider(false);
                health.AddHealth(1);
                OnCollect?.Invoke();
                GameManager.Instance.AddLife();
                GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
                Destroy(go, 0.7f);
            }


        }
    }

}
