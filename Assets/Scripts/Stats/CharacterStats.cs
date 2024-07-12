using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat strenght;
    public Stat damage;
    public Stat maxHp;
    [SerializeField] private int currentHp;
    protected virtual void Start()
    {
        currentHp = maxHp.GetValue();
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

}
