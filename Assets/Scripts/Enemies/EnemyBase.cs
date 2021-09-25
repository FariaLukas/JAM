using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{
    public EnemyData data;
    public Health health { get; set; }
    protected NavMeshAgent agent;
    protected PlayerControll player;
    protected Health _playerHealth;
    
    private void Awake()
    {
        Init();

    }

    protected virtual void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = FindObjectOfType<PlayerControll>();
        _playerHealth = player.GetComponent<Health>();

        agent.speed = data.speed;
        agent.stoppingDistance = data.range;

        health = GetComponent<Health>();
        health.SetInitialLife(data.initialLife);
        health.OnKill += AddScore;

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

    public float GetRemainingDistance()
    {
        return agent.remainingDistance;
    }

    public float GetDelay()
    {
        return data.delay;
    }

    private void AddScore()
    {
        GameManager.Instance.AddScore(data.score);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            _playerHealth.Damage(data.fisicalDamage);

        }
    }

}
