using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyMagicAttack
{
    public float prepDuration;
    public GameObject MagicPrefab;
    public float attackRange;
    public float baseWeight => 1/attackRange;
}
