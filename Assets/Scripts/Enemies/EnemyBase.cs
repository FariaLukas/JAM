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
    protected bool dead;

    private void Awake()
    {
        Init();

    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        health.OnKill -= AddScore;
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
        health.OnKill += OnDie;

        animator = GetComponent<Animator>();

        dead = false;

    }

    public virtual void FollowPlayer()
    {
        if (dead) return;
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
        if (dead) return;
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
        Vector3 position = transform.position;
        position.y += 0.1f;
        GameManager.Instance.AddScore(data.score, position);
    }

    protected virtual void OnDie()
    {
        if (!dead)
            AddScore();
        dead = true;
        Stopped();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            if (playerHealth)
            {
                playerHealth.Damage(data.fisicalDamage);
                GameManager.Instance.PlayerDamage();
            }

        }
    }

}
