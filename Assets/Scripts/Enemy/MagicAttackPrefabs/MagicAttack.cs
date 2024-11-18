using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [HideInInspector] public Enemy enemy;
    public void getEnemy(Enemy _enemy){
        enemy = _enemy;
    }

}
