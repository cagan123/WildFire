using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlotUI : ItemSlotUI
{
    private void OnEnable()
    {
        UpdateSlot(item);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        EquipmentItemData craftData = item.data as EquipmentItemData;

        Inventory.instance.CanCraft(craftData, craftData.craftingMaterials);
    }
}
