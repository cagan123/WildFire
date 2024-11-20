using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [HideInInspector] public Enemy enemy;
    private MagicDamageSource damageSource => GetComponentInChildren<MagicDamageSource>();
    public void getEnemy(Enemy _enemy){
        enemy = _enemy;
    }
    public virtual void Update(){
        if(damageSource.damageDone){
            Destroy(this.gameObject);
        }
    }
    

}
