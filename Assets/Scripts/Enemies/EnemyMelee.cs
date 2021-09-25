using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyBase
{
    private bool _finished;

    public override void Attack()
    {
        _playerHealth.Damage(data.damage);

    }

    public void StartCooldown()
    {
        _finished = false;
        StartCoroutine(Wait());

    }

    public bool Finished()
    {
        return _finished;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(data.delay);
        _finished = true;
    }
}
