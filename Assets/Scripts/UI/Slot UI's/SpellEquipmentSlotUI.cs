using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SpellEquipmentSlotUI : ItemSlotUI
{
    [SerializeField] public KeyCode keycode;
    public override void OnPointerDown(PointerEventData eventData)
    {
        if(item == null || item.data == null)
            return;

        if(keycode == KeyCode.Mouse0){
            Inventory.instance.RemoveFromLeftClickSlot();
        }
        if(keycode == KeyCode.Mouse1){
            Inventory.instance.RemoveFromRightClickSlot();
        }
        if(keycode == KeyCode.Q){
            Inventory.instance.RemoveFromQSlot();
        }
        if(keycode == KeyCode.E){
            Inventory.instance.RemoveFromESlot();
        }
        if(keycode == KeyCode.Space){
            Inventory.instance.RemoveFromDashSlot();
        }
        CleanUpSlot();
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        UI.spellTooltip.ShowToolTip(item.data as SpellData, transform);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        UI.spellTooltip.HideToolTip();
    }

}
