using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem item;

    public void UpdateSlot(InventoryItem _newitem)
    {
        item = _newitem;
        itemImage.color = Color.white;
        if (item != null)
        {
            itemImage.sprite = item.data.icon;
            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void CleanUpSlot(){
        item = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {   
        if(item.data != null){
            if(item.data.itemType == ItemType.Equipment){
                Inventory.instance.EquipItem(item.data);
            }
        }
        else{
            Debug.LogWarning("Item data is null when clicking!");
            return;
        }
    }
}
