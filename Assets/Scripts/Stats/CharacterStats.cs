using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
public enum StatType{
        damage,
        vitality,
        stamina
    }
public class CharacterStats : MonoBehaviour
{
    
    public Stat strenght;
    public Stat damage;
    public Stat maxHp;
    public int currentHp;
    public Stat maxStamina;
    public float currentStamina;
    public bool delayStaminaRecovery;
    public EntityVFX VFX;
    public bool isDead{ get; private set; }

    protected virtual void Start()
    {
        currentHp = maxHp.GetValue();
        currentStamina = maxStamina.GetValue();
        VFX = GetComponent<EntityVFX>();
    }
    public virtual void DoDamage(CharacterStats _targetStat){
        int totalDamage = damage.GetValue() + strenght.GetValue();
        _targetStat.TakeDamage(totalDamage);
    }
    public virtual void TakeDamage(int _damage){
        currentHp -= _damage;
        if(currentHp <= 0){
            Die();
        }
    }
    protected virtual void Die(){
        isDead = true;
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
    public virtual void UseFloatStamina(float _amount){
        if(currentStamina < _amount){
            return;
        }
        else{
            currentStamina -= _amount;
        }       
    }
    public virtual bool HasEnoughStamina(int _amount){
        if(currentStamina < _amount){
            VFX.NotEnoughStaminaVFX();
            return false;
        }
        else{
            UseStamina(_amount);
            return true;
        }
    }
    public Stat GetStat(StatType _statType){
        if(_statType == StatType.damage) return damage;
        else if (_statType == StatType.vitality) return maxHp;
        else if (_statType == StatType.stamina) return maxStamina;
        
        return null;
    }
}
