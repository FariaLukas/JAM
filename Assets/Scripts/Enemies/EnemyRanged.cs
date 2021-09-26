using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EnemyBase
{
    private Weapon weapon;
    private bool _canAttack;
    private Renderer _render;
    protected override void Init()
    {
        base.Init();
        weapon = GetComponent<Weapon>();
        weapon.OverrideRatio(data.delay);
        if (!_render)
            _render = GetComponent<SpriteRenderer>();
    }

    public override void Attack()
    {
        if (dead) return;
        if (!_canAttack) return;

        if (player)
            weapon.Shoot(player.transform, data.damage, animator, data.attackTrigger);
    }

    private void OnBecameVisible()
    {
        Invoke(nameof(Activate), 1f);
        print("A");
    }

    private void Activate()
    {
        if (_render.isVisible)
            _canAttack = true;

        print("B");
    }

    private void OnBecameInvisible()
    {
        _canAttack = false;
    }
}
