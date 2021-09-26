using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCollectable : Collectable
{
    public GameObject prefab;
    public float timeToDisapear = 20f;

    private bool startCountDown;
    private float timer;

    private void OnEnable()
    {
        startCountDown = true;
    }

    private void Update()
    {
        if (startCountDown)
        {
            timer += Time.deltaTime;
            if (timer >= timeToDisapear)
                Destroy(gameObject);
        }

    }

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
