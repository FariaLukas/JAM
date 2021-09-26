using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public GlobalInt life;
    private Health _health;
    private bool finished;

    private void Awake()
    {
        life.value = 3;
        _health = GetComponent<Health>();
        _health.SetInitialLife(life.value);
    }

    private void Update()
    {
        life.value = (int)_health.currentLife;
        if (life.value <= 0 && !finished)
        {
            finished = true;
            GameManager.Instance.PlayerDie();
        }
    }


}


