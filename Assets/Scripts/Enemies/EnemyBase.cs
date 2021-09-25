using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public EnemyData data;
    public PlayerMoviment player;
    protected NavMeshAgent agent;

    private void Awake()
    {
        Init();

    }

    protected virtual void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = FindObjectOfType<PlayerMoviment>();

    }

    private void Update()
    {
        FollowPlayer();
    }

    public virtual void FollowPlayer()
    {
        agent.SetDestination(player.transform.position);

    }

    public virtual void Attack()
    {


    }

    public virtual void Stopped()
    {
        agent.SetDestination(transform.position);

    }

    public float Distance()
    {
        return agent.remainingDistance;
    }

}
