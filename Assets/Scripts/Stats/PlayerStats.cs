using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    private FireSpirit fireSpirit;

    protected override void Start(){
        base.Start();
        player = GetComponent<Player>();
        fireSpirit = FireSpiritManager.instance.fireSpirit;
    }
    public override void TakeDamage(int _damage)
    {
        if(player.stateMachine.currentState == player.dashState){
            return;
        }
        base.TakeDamage(_damage);
        player.knockbackDir = player.KnockbackDirection();
        player.DamageEffect(_damage, player.transform);
    }
    protected override void Die()
    {
        base.Die();
        player.Die();
    }
    public override void StaminaRecovery(){
        if(delayStaminaRecovery){
            StartCoroutine("DelayRecoveryRoutine");
            return;
        }
        else if(currentStamina >= maxStamina.GetValue()){
            currentStamina = maxStamina.GetValue();
        }
        else{
            currentStamina += player.staminaRecoveryRate * Time.deltaTime;
        }
    }
    public override void UseStamina(int _amount){
        base.UseStamina(_amount);
        delayStaminaRecovery = true;
    }
    public override bool HasEnoughStamina(int _amount)
    {
        return base.HasEnoughStamina(_amount);
    }
    public IEnumerator DelayRecoveryRoutine(){
        yield return new WaitForSeconds(1f);
        delayStaminaRecovery = false;
    }
    

}
