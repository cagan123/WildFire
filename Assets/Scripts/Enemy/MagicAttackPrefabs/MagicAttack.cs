using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [HideInInspector] public Enemy enemy;
    public MagicDamageSource damageSource => GetComponentInChildren<MagicDamageSource>();
    public void getEnemy(Enemy _enemy){
        enemy = _enemy;
    }
    public void DestroyOnDamage(){
        if(damageSource.damageDone){
            Destroy(gameObject);
        }
    }
    

}
