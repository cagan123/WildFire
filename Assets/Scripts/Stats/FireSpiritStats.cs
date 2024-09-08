using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritStats : CharacterStats
{
    private FireSpirit fireSpirit;
    private Player player;
    protected override void Start(){
        base.Start();
        fireSpirit = GetComponent<FireSpirit>();
        player = PlayerManager.instance.player;
    }
    public override void UseStamina(int _amount){
        player.stats.delayStaminaRecovery = true;
        if(player.stats.currentStamina < _amount){
            return;
        }
        player.stats.currentStamina -= _amount;
    }
    public override bool HasEnoughStamina(int _amount)
    {
        if(player.stats.currentStamina < _amount){
            return false;
        }
        else{
            UseStamina(_amount);
            return true;
        }
    }
}
