using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInventoryUIManager : MonoBehaviour
{
   [SerializeField] private Transform inventoryGrid; // The parent object for inventory slots
    [SerializeField] private GameObject spellUIPrefab; // Prefab for each spell's UI representation

    private void Start()
    {
        PopulateInventory();
    }

    private void PopulateInventory()
    {
        // Get all spells from the inventory
        List<Spell> spells = SpellInventory.Instance.GetAllSpells();

        foreach (Spell spell in spells)
        {
            // Instantiate a new UI element for each spell
            GameObject spellIcon = Instantiate(spellUIPrefab, inventoryGrid);

            // Set the spell's data in the UI
            SpellUI spellUI = spellIcon.GetComponent<SpellUI>();
            if (spellUI != null)
            {
                spellUI.SetSpell(spell);
            }
        }
    }
}
