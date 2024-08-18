using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpritAC : MonoBehaviour
{
    private FireSpirit fireSpirit => GetComponentInParent<FireSpirit>();

    private void AnimationTrigger()
        {
            fireSpirit.AnimationFinishTrigger();
        }
    
}
