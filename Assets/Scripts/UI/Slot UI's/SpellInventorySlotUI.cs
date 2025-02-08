using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using JetBrains.Annotations;
public class SpellInventorySlotUI : ItemSlotUI
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
            return;
        UI.spellTooltip.ShowToolTip(item.data as SpellData, transform);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        UI.spellTooltip.HideToolTip();
    }

    public override void OnPointerDown(PointerEventData eventData)
    { 
        if(item == null || item.data == null)
            return;
        base.OnPointerEnter(eventData);
        UI.spellKeyUI.ShowToolTip(item.data as SpellData);
        UI.spellKeyUI.currentSpell = item.data as SpellData;
    }
}
