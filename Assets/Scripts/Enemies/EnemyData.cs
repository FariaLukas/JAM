using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Enemies")]
public class EnemyData : ScriptableObject
{
    public float speed;
    public float range;
    public float delay;

    public float initialLife;
    public int score;

    [Header("Attack")]
    public float damage;
    public float fisicalDamage;

    [Header("Animation")]
    public string attackTrigger = "Attack";
    public string speedTrigger = "Speed";
    public string dieTrigger = "Die";

}
