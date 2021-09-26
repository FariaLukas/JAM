using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyBase
{
    private bool _finished;

    public override void Attack()
    {
        base.Attack();

        if (dead) return;

        if (Vector2.Distance(player.transform.position, transform.position) > data.range) return;

        if (playerHealth)
        {

            if (animator)
                animator.SetTrigger(data.attackTrigger);

            playerHealth.Damage(data.damage, gameObject);
            GameManager.Instance.PlayerDamage();
           
        }

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
