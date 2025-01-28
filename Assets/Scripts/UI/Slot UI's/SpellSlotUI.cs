using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlotUI : MonoBehaviour
{
    [SerializeField] private Image spellIconImage; // The UI Image for the slot
    [SerializeField] private KeyCode assignedKey; // The key this slot is bound to

    private Spell equippedSpell; // The currently equipped spell

    public void SetSpell(Spell spell)
    {
        equippedSpell = spell;

        if (spell != null)
        {
            spellIconImage.sprite = spell.Icon; // Update the slot's icon
            spellIconImage.enabled = true;     // Make sure the icon is visible
        }
        else
        {
            ClearSlot();
        }
    }
    public void Update(){
        
    }

    public void ClearSlot()
    {
        equippedSpell = null;
        spellIconImage.sprite = null; // Clear the icon
        spellIconImage.enabled = false; // Hide the icon
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Get the SpellUI component from the dragged object
        SpellUI spellUI = eventData.pointerDrag.GetComponent<SpellUI>();
        if (spellUI != null)
        {
            SetSpell(spellUI.assignedSpell); // Assign the dragged spell to this slot
            spellUI.transform.SetParent(transform); // Move the dragged object to this slot
            spellUI.transform.localPosition = Vector2.zero; // Center the dragged object in the slot
        }
    }

    public Spell GetEquippedSpell()
    {
        return equippedSpell;
    }
}
