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
    protected Health playerHealth;
    protected Animator animator;

    private void Awake()
    {
        Init();

    }

    private void OnEnable()
    {
        Init();
    }

    protected virtual void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = FindObjectOfType<PlayerControll>();
        if (player)
            playerHealth = player.GetComponent<Health>();

        agent.speed = data.speed;
        agent.stoppingDistance = data.range;

        health = GetComponent<Health>();
        health.SetInitialLife(data.initialLife);
        health.OnKill += AddScore;

        animator = GetComponent<Animator>();

    }

    public virtual void FollowPlayer()
    {
        agent.SetDestination(player.transform.position);

    }

    protected virtual void Update()
    {
        if (player)
        {
            float side = transform.position.x > player.transform.position.x ? -1 : 1;
            if (animator)
                animator.SetFloat(data.speedTrigger, side);

        }

    }

    public virtual void Attack()
    {
        if (animator)
            animator.SetTrigger(data.attackTrigger);
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
            if (playerHealth)
                playerHealth.Damage(data.fisicalDamage);


        }
    }

}
