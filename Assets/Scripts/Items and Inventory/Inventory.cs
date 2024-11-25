using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData, InventoryItem> inventoryDictianory;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    private ItemSlotUI[] itemSlot;
    public void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public void Start(){
        inventoryItems = new List<InventoryItem>();
        inventoryDictianory = new Dictionary<ItemData, InventoryItem>();
        itemSlot = inventorySlotParent.GetComponentsInChildren<ItemSlotUI>();
    }
    private void UpdateSlotUI(){
        for(int i = 0; i< inventoryItems.Count; i++){
            itemSlot[i].UpdateSlot(inventoryItems[i]);
        }
    }

    public void AddItem(ItemData _item){
        if(inventoryDictianory.TryGetValue(_item, out InventoryItem value)){
            value.AddStack();
        }
        else{
            InventoryItem newItem = new InventoryItem(_item);
            inventoryItems.Add(newItem);
            inventoryDictianory.Add(_item, newItem);
        }
        UpdateSlotUI();
    }
    public void RemoveItem(ItemData _item){
        if(inventoryDictianory.TryGetValue(_item, out InventoryItem value)){
            if(value.stackSize <= 1){
                inventoryItems.Remove(value);
                inventoryDictianory.Remove(_item);
            }
            else{
                value.RemoveStack();
            }
        }
        UpdateSlotUI();
    }
}
