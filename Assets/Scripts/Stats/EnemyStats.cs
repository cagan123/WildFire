using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    Enemy enemy;
    private ItemDrop dropSystem;
    public bool isDamaged = false;
    [Header("Level Details")]
    [SerializeField] private int level;

    [Range(0f,1f)]
    [SerializeField] private float precentageModifier = .2f;
    protected override void Start()
    {
        Modify(damage);
        Modify(maxHp);
        // add some more here when needed
        base.Start();
        enemy = GetComponent<Enemy>();
        dropSystem = GetComponent<ItemDrop>();     
    }
    private void Modify(Stat _stat){
        for(int i = 0; i < level; i++){
            float modifier = _stat.GetValue() * precentageModifier;
            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }
    public override void TakeDamage(int _damage, int _poiseDamage)
    {
        base.TakeDamage(_damage, _poiseDamage);
        enemy.knockbackDir = -enemy.EnemyToPlayerDirection();
        enemy.DamageEffect(_damage, enemy.transform);
        isDamaged = true;
    }
    protected override void Die()
    {
        base.Die();
        enemy.Die();
        dropSystem.GenerateDrop();
    }
    public bool DamageTaken(){
        return isDamaged;
    }
}
