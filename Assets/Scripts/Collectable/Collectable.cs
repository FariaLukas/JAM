using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    private Collider2D _collider;
    public string collisionTag;
    public UnityEvent OnCollect;
    private void OnEnable()
    {
        if (!_collider)
            _collider = GetComponent<Collider2D>();
        ChangeCollider(true);
    }

    private void OnDisable()
    {
        ChangeCollider(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }

    public void ChangeCollider(bool active)
    {
        _collider.enabled = active;
    }
}
