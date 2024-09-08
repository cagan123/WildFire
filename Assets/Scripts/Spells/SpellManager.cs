using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager instance;

    public FireballSpell fireballSpell { get; private set; }

    private void Awake(){
        if(instance != null ){
            Destroy(instance.gameObject);
        }
        else{
            instance = this;
        }
    }
    private void Start(){
        fireballSpell = GetComponent<FireballSpell>();
    }
    
}
