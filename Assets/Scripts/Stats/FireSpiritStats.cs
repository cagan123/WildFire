using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritStats : CharacterStats
{
    private FireSpirit fireSpirit;
    protected override void Start(){
        base.Start();
        fireSpirit = GetComponent<FireSpirit>();
    }
}
