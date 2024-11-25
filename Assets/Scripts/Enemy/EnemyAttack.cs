using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttack
{
    public float attackDashSpeed;
    public float prepDuration;
    public Collider2D damageCollider;
    public float attackRange;
    public float baseWeight => 1/attackRange;
}
