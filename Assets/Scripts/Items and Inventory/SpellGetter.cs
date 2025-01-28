using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGetter : MonoBehaviour
{
    Dictionary<KeyCode, Spell> spells = new Dictionary<KeyCode, Spell>();
    public SpellSlotUI[] spellSlotUIs;
    public void Start(){
        spellSlotUIs = GetComponentsInChildren<SpellSlotUI>();
    }
    public void AddSpell(KeyCode key, Spell spell)
    {
        spells.Add(key, spell);
    }
    
}
