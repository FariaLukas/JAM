using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyData data;

    public Player player;

    private void Awake()
    {
        Init();

    }

    protected virtual void Init()
    {
        player = FindObjectOfType<Player>();

    }

    public virtual void Movement()
    {
        transform.position = Vector2.Lerp(transform.position, player.transform.position, data.speed);

    }

    public virtual void Attack()
    {


    }

    public virtual void Stopped()
    {

    }

    public float Distance()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

}
