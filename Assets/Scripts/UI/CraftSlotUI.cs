using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlotUI : ItemSlotUI
{
    public void OnEnable(){
        UpdateSlot(item);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        EquipmentItemData craftItem = item.data as EquipmentItemData;
        Inventory.instance.CanCraft(craftItem, craftItem.craftingMaterials);
    
    }
}
