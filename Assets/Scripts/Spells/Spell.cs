using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    [SerializeField] protected int staminaAmountUsed;
    protected float cooldownTimer;
 
    public virtual void Update(){
        cooldownTimer -= Time.deltaTime;
    }
    public virtual bool CanUseSpell(){
        if(cooldownTimer <0){
            UseSpell();
            cooldownTimer = cooldown;
            return true;
        }
        else{
            return false;
        }
    }

    public virtual void UseSpell(){
        
    }

}
