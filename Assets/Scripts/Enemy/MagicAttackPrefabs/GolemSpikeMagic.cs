using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class GolemSpikeMagic : MagicAttack
{
    private Vector3 direction;
    void Start(){
        direction = (PlayerManager.instance.player.transform.position - enemy.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle+90);
    }
    public void Update(){
        DestroyOnDamage();
    }
}
