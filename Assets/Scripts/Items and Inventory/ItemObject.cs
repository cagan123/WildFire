using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;
    private void SetupVisuals()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item object - " + itemData.itemName;
    }
    private void OnValidate(){
        SetupVisuals();
    }

    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetupVisuals();
    }

    public void PickupItem()
    {
        if(!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
            return;
            
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
