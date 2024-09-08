using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat strenght;
    public Stat damage;
    public Stat maxHp;
    public int currentHp;
    public Stat maxStamina;
    public float currentStamina;
    public bool delayStaminaRecovery;
    protected virtual void Start()
    {
        currentHp = maxHp.GetValue();
        currentStamina = maxStamina.GetValue();
    }
    public virtual void DoDamage(CharacterStats _targetStat){
        int totalDamage = damage.GetValue() + strenght.GetValue();
        _targetStat.TakeDamage(totalDamage);
    }
    public virtual void TakeDamage(int _damage){
        currentHp -= _damage;
        Debug.Log(_damage);
        if(currentHp <= 0){
            Die();
        }
    }
    protected virtual void Die(){

    }
    public virtual void StaminaRecovery(){
    }
    public virtual void UseStamina(int _amount){
        if(currentStamina < _amount){
            return;
        }
        else{
            currentStamina -= _amount;
        }       
    }
    public virtual bool HasEnoughStamina(int _amount){
        if(currentStamina < _amount){
            return false;
        }
        else{
            UseStamina(_amount);
            return true;
        }
    }
}
