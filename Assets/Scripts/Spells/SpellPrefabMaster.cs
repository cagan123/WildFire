using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPrefabMaster : MonoBehaviour
{
    [HideInInspector] public FireSpirit fireSpirit;
    public void getFirespirit(FireSpirit _fireSpirit){
        fireSpirit = _fireSpirit;
    }
}
