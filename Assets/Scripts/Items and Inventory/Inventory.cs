using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictianory;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictianory;

    public List<InventoryItem> equipment;
    public Dictionary<EquipmentItemData, InventoryItem> equipmentDictionary;


    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    private ItemSlotUI[] inventoryItemSlot;
    private ItemSlotUI[] stashItemSlot;
    public void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public void Start(){
        inventory = new List<InventoryItem>();
        inventoryDictianory = new Dictionary<ItemData, InventoryItem>();
        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<ItemSlotUI>();

        stash = new List<InventoryItem>();
        stashDictianory = new Dictionary<ItemData, InventoryItem>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<ItemSlotUI>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<EquipmentItemData, InventoryItem>();

    }
    public void EquipItem(ItemData _item)
    {
        EquipmentItemData newEquipement = _item as EquipmentItemData;
        InventoryItem newItem = new InventoryItem(newEquipement);

        EquipmentItemData oldEquipment = null;

        foreach (KeyValuePair<EquipmentItemData, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipement.equipmentType)
            {
                oldEquipment = item.Key;
            }
        }
        if(oldEquipment != null){
            UnequipItem(oldEquipment);
            AddItem(oldEquipment);
        }
        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipement, newItem);
        RemoveItem(_item);
        UpdateSlotUI();
    }

    private void UnequipItem(EquipmentItemData itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
        }
    }

    private void UpdateSlotUI(){
        
        for(int i = 0; i< inventoryItemSlot.Length; i++){
            inventoryItemSlot[i].CleanUpSlot();
        }
        for(int i = 0; i< stashItemSlot.Length; i++){
            stashItemSlot[i].CleanUpSlot();
        }

        for(int i = 0; i< inventory.Count; i++){
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }
        for(int i = 0; i< stash.Count; i++){
            stashItemSlot[i].UpdateSlot(stash[i]);
        }    
    }

    public void AddItem(ItemData _item)
    {

        if (_item.itemType == ItemType.Equipment)
        {
            AddToInventory(_item);
        }
        else if(_item.itemType == ItemType.Material)
        {
            AddToStash(_item);
        }

        UpdateSlotUI();
    }

    private void AddToStash(ItemData _item)
    {
        if (stashDictianory.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictianory.Add(_item, newItem);
        }
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictianory.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictianory.Add(_item, newItem);
        }
    }

    public void RemoveItem(ItemData _item){
        if(inventoryDictianory.TryGetValue(_item, out InventoryItem value)){
            if(value.stackSize <= 1){
                inventory.Remove(value);
                inventoryDictianory.Remove(_item);
            }
            else{
                value.RemoveStack();
            }
        }
        if(inventoryDictianory.TryGetValue(_item, out InventoryItem stashvalue)){
            if(stashvalue.stackSize <= 1){
                stash.Remove(value);
                stashDictianory.Remove(_item);
            }
            else{
                value.RemoveStack();
            }
        }
        UpdateSlotUI();
    }
}
