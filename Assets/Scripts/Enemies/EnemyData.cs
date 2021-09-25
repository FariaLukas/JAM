using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Enemies")]
public class EnemyData : ScriptableObject
{
    public float speed;
    public float range;
    public float damage;
}
