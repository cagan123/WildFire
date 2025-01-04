using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
public enum StatType{
        damage,
        vitality,
        stamina,
        poise
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
    public Stat Poise;
    public Stat PoiseDamage;
    public float currentPoise;
    public float poiseRecoveryRate;
    public bool poiseBroken;
    public EntityVFX VFX;
    public bool isDead{ get; private set; }

    protected virtual void Start()
    {
        currentHp = maxHp.GetValue();
        currentStamina = maxStamina.GetValue();
        currentPoise = Poise.GetValue();
        VFX = GetComponent<EntityVFX>();
    }
    public virtual void DoDamage(CharacterStats _targetStat){
        int totalDamage = damage.GetValue() + strenght.GetValue();
        _targetStat.TakeDamage(totalDamage, PoiseDamage.GetValue());
    }
    public virtual void TakeDamage(int _damage, int _poiseDamage){
        currentHp -= _damage;
        if(currentHp <= 0){
            Die();
        }
        TakePoiseDamage(_poiseDamage);
    }
    protected virtual void Die(){
        isDead = true;
    }
    public virtual void StaminaRecovery(){
    }
    public virtual void PoiseRecovery(){
        if(currentPoise >= Poise.GetValue()){
            return;
        }
        currentPoise += poiseRecoveryRate * Time.deltaTime;
    }
    public void ResetPoise(){
        currentPoise = Poise.GetValue();
        poiseBroken = false;
    }
    public virtual void TakePoiseDamage(int _amount){// take poise damage from somewhere
        if(currentPoise < _amount){
            poiseBroken = true;
            return;
        }
        else{
            currentPoise -= _amount;
        }
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
