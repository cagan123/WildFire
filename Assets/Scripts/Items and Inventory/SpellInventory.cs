using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInventory : MonoBehaviour
{
    public static SpellInventory Instance { get; private set; } // Singleton for global access

    public List<Spell> spells = new List<Spell>(); // Store all spells the player owns

    private void Awake()
    {
        // Ensure only one instance of the inventory exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add a new spell to the inventory
    public void AddSpell(Spell spell)
    {
        spells.Add(spell);
    }

    // Get all spells currently in the inventory
    public List<Spell> GetAllSpells()
    {
        return spells;
    }
}

