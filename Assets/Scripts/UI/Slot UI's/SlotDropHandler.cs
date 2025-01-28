using UnityEngine;
using UnityEngine.EventSystems;
public class SlotDropHandler :  MonoBehaviour, IDropHandler
{
    public Spell EquippedSpell { get; private set; } // The spell currently assigned to this slot

    public void OnDrop(PointerEventData eventData)
    {
        // Get the dragged object
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject == null) return;

        // Get the SpellUI component from the dragged object
        SpellUI draggedSpellUI = draggedObject.GetComponent<SpellUI>();
        if (draggedSpellUI == null) return;

        // Assign the spell to this slot
        EquippedSpell = draggedSpellUI.GetAssignedSpell();

        // Snap the dragged object into the slot's position
        draggedObject.transform.SetParent(transform);
        draggedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
