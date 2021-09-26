using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EnemyBase
{
    private Weapon weapon;

    protected override void Init()
    {
        base.Init();
        weapon = GetComponent<Weapon>();
        weapon.OverrideRatio(data.delay);
    }

    public override void Attack()
    {
        if (player)
            weapon.Shoot(player.transform, data.damage, animator, data.attackTrigger);
    }
}
