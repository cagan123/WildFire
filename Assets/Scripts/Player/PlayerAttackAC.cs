using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAC : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();
    private void AnimationTrigger()
    {
        player.AnimationFinishTrigger();
    }
}
