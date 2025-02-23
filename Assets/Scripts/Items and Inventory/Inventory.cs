using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory instance;

    public List<ItemData> startingItems;

    public List<InventoryItem> equipment;
    public Dictionary<EquipmentItemData, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictianory;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictianory;

    public List<InventoryItem> spellInventory;
    public Dictionary<SpellData, InventoryItem> spellDictionary;



    [Header("Inventory UI")]

    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equpmentSlotParent;
    [SerializeField] private Transform statSlotParent;
    [SerializeField] private Transform spellInventorySlotParent;
    
    [Header("Spell Slot UI")]
    [SerializeField] private SpellEquipmentSlotUI leftClickSlot;
    [SerializeField] private SpellEquipmentSlotUI rightClickSlot;
    [SerializeField] private SpellEquipmentSlotUI dashSlot;
    [SerializeField] private SpellEquipmentSlotUI eSlot;
    [SerializeField] private SpellEquipmentSlotUI qSlot;

    private ItemSlotUI[] inventoryItemSlot;
    private ItemSlotUI[] stashItemSlot;
    private EquipentSlotUI[] equipmentSlot;
    private StatSlotUI[] statSlot;
    private SpellInventorySlotUI[] spellInventorySlot;
    


    [Header("Items cooldown")]
    private float lastTimeUsedFlask;
    private float lastTimeUsedArmor;

    private float flaskCooldown;
    private float armorCooldown;

    [Header("Data base")]
    public List<InventoryItem> loadedItems;
    public List<EquipmentItemData> loadedEquipment;
    public SpellData loadedLeftSpell;
    public SpellData loadedRightSpell;
    public SpellData loadedDashSpell;
    public SpellData loadedQSpell;
    public SpellData loadedESpell;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictianory = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictianory = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<EquipmentItemData, InventoryItem>();

        spellInventory = new List<InventoryItem>();
        spellDictionary = new Dictionary<SpellData, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<ItemSlotUI>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<ItemSlotUI>();
        equipmentSlot = equpmentSlotParent.GetComponentsInChildren<EquipentSlotUI>();
        spellInventorySlot = spellInventorySlotParent.GetComponentsInChildren<SpellInventorySlotUI>();


        statSlot = statSlotParent.GetComponentsInChildren<StatSlotUI>();

        AddStartingItems();
    }

    private void AddStartingItems()
    {
        if(loadedLeftSpell != null){
            AddToLeftClickSlot(loadedLeftSpell);
        }
        if(loadedRightSpell != null){
            AddToRightClickSlot(loadedRightSpell);
        }
        if(loadedDashSpell != null){
            AddToDashSlot(loadedDashSpell);
        }
        if(loadedESpell != null){
            AddToESlot(loadedESpell);
        }
        if(loadedQSpell != null){
            AddToQSlot(loadedQSpell);
        }
        foreach (EquipmentItemData item in loadedEquipment){
            EquipItem(item);
        }
        if(loadedItems.Count > 0){
            foreach(InventoryItem item in loadedItems){
                for(int i = 0; i < item.stackSize; i++){
                    AddItem(item.data);
                }
            }
            return;
        }
        for (int i = 0; i < startingItems.Count; i++)
        {
            AddItem(startingItems[i]);
        }
    }
    
    #region Spell Equipment Slots

    public void AddToLeftClickSlot(SpellData _spell){
        var newItem = new InventoryItem(_spell);

        if(leftClickSlot.item != null && leftClickSlot.item.data != null){
            RemoveFromLeftClickSlot();
        }
        
        leftClickSlot.UpdateSlot(newItem);
        RemoveItem(_spell);
    }
    public void RemoveFromLeftClickSlot(){
        if(leftClickSlot.item == null || leftClickSlot.item.data == null)
            return;
        AddItem(leftClickSlot.item.data);
        leftClickSlot.CleanUpSlot();
    }
    public void AddToRightClickSlot(SpellData _spell){
        var newItem = new InventoryItem(_spell);

        if(rightClickSlot.item != null && rightClickSlot.item.data != null){
            RemoveFromRightClickSlot();
        }
        
        rightClickSlot.UpdateSlot(newItem);
        RemoveItem(_spell);
    }
    public void RemoveFromRightClickSlot(){
        if(rightClickSlot.item == null || rightClickSlot.item.data == null)
            return;
        AddItem(rightClickSlot.item.data);
        rightClickSlot.CleanUpSlot();
    }
    public void AddToDashSlot(SpellData _spell){
        var newItem = new InventoryItem(_spell);

        if(dashSlot.item != null && dashSlot.item.data != null){
            RemoveFromDashSlot();
        }
        
        dashSlot.UpdateSlot(newItem);
        RemoveItem(_spell);
    }
    public void RemoveFromDashSlot(){
        if(dashSlot.item == null || dashSlot.item.data == null)
            return;
        AddItem(dashSlot.item.data);
        dashSlot.CleanUpSlot();
    }
    public void AddToESlot(SpellData _spell){
        var newItem = new InventoryItem(_spell);

        if(eSlot.item != null && eSlot.item.data != null){
            RemoveFromESlot();
        }
        
        eSlot.UpdateSlot(newItem);
        RemoveItem(_spell);
    }
    public void RemoveFromESlot(){
        if(eSlot.item == null || eSlot.item.data == null)
            return;
        AddItem(eSlot.item.data);
        eSlot.CleanUpSlot();
    }
    public void AddToQSlot(SpellData _spell){
        var newItem = new InventoryItem(_spell);

        if(qSlot.item != null && qSlot.item.data != null){
            RemoveFromQSlot();
        }
        
        qSlot.UpdateSlot(newItem);
        RemoveItem(_spell);
    }
    public void RemoveFromQSlot(){
        if(qSlot.item == null || qSlot.item.data == null)
            return;
        AddItem(qSlot.item.data);
        qSlot.CleanUpSlot();
    }
    #endregion
    public void EquipItem(ItemData _item)
    {
        EquipmentItemData newEquipment = _item as EquipmentItemData;
        InventoryItem newItem = new InventoryItem(newEquipment);

        EquipmentItemData oldEquipment = null;

        foreach (KeyValuePair<EquipmentItemData, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
                oldEquipment = item.Key;
        }

        if (oldEquipment != null)
        {
            UnequipItem(oldEquipment);
            AddItem(oldEquipment);
        }


        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        RemoveItem(_item);

        UpdateSlotUI();
    }

    public void UnequipItem(EquipmentItemData itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<EquipmentItemData, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].slotType)
                    equipmentSlot[i].UpdateSlot(item.Value);
            }
        }

        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }
        for (int i = 0; i < spellInventorySlot.Length; i++)
        {
            spellInventorySlot[i].CleanUpSlot();
        }


        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stash[i]);
        }
        
        for (int i = 0; i < spellInventory.Count; i++)
        {
            spellInventorySlot[i].UpdateSlot(spellInventory[i]);
        }
        UpdateStatsUI();
    }
    public void UpdateStatsUI()
    {
        for (int i = 0; i < statSlot.Length; i++) // update info of stats in character UI
        {
            statSlot[i].UpdateStatValueUI();
        }
    }

    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment && CanAddItem()){
            AddToInventory(_item);
        }
        else if (_item.itemType == ItemType.Material){
            AddToStash(_item);
        }   
        else if (_item.itemType == ItemType.Spell){
            AddToSpellInventory(_item);
        }
        UpdateSlotUI();
    }
    public bool CanAddItem()
    {
        if (inventory.Count >= inventoryItemSlot.Length)
        {
            return false;
        }

        return true;
    }
    public void AddToSpellInventory(ItemData _item){
        if(spellDictionary.TryGetValue(_item as SpellData, out InventoryItem value)){
            value.AddStack();
        }
        else{
            InventoryItem newItem = new InventoryItem(_item);
            spellInventory.Add(newItem);
            spellDictionary.Add(_item as SpellData, newItem);
        }
        
    }
    private void AddToStash(ItemData _item)
    {
        if (stashDictianory.TryGetValue(_item, out InventoryItem value)){
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

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictianory.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictianory.Remove(_item);
            }
            else
                value.RemoveStack();
        }


        if (stashDictianory.TryGetValue(_item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictianory.Remove(_item);
            }
            else
                stashValue.RemoveStack();
        }
        
        if(_item as SpellData != null){
            if(spellDictionary.TryGetValue(_item as SpellData, out InventoryItem spellValue)){
            
                if(spellValue.stackSize <= 1){
                    spellInventory.Remove(spellValue);
                    spellDictionary.Remove(_item as SpellData);
                }
                else{
                    spellValue.RemoveStack();
                }
            }
        }
            

        UpdateSlotUI();
    }

    public bool CanCraft(EquipmentItemData _itemToCraft, List<InventoryItem> _requiredMaterials)
    {
        List<InventoryItem> materialsToRemove = new List<InventoryItem>();

        for (int i = 0; i < _requiredMaterials.Count; i++)
        {
            if (stashDictianory.TryGetValue(_requiredMaterials[i].data, out InventoryItem stashValue))
            {
                if (stashValue.stackSize < _requiredMaterials[i].stackSize)
                {
                    Debug.Log("not enough materials");
                    return false;
                }
                else
                {
                    materialsToRemove.Add(stashValue);
                }

            }
            else
            {
                Debug.Log("not enough materials");
                return false;
            }
        }


        for (int i = 0; i < materialsToRemove.Count; i++)
        {
            RemoveItem(materialsToRemove[i].data);
        }

        AddItem(_itemToCraft);
        Debug.Log("Here is your item " + _itemToCraft.name);

        return true;
    }

    public List<InventoryItem> GetEquipmentList() => equipment;

    public List<InventoryItem> GetStashList() => stash;

    public EquipmentItemData GetEquipment(EquipmentType _type)
    {
        EquipmentItemData equipedItem = null;

        foreach (KeyValuePair<EquipmentItemData, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == _type)
                equipedItem = item.Key;
        }

        return equipedItem;
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, int> pair in _data.inventory)
        {
            foreach (var item in GetItemDataBase())
            {
                if (item != null && item.itemID == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = pair.Value;

                    loadedItems.Add(itemToLoad);
                }
            }
        }

        foreach (string loadedItemId in _data.equipmentId)
        {
            foreach (var item in GetItemDataBase())
            {
                if (item != null && loadedItemId == item.itemID)
                {
                    loadedEquipment.Add(item as EquipmentItemData);
                }
            }
        }

        foreach(var item in GetItemDataBase()){
            if(item != null && _data.leftSpellSlotID == item.itemID){
                loadedLeftSpell = item as SpellData;
            }
        }
        foreach(var item in GetItemDataBase()){
            if(item != null && _data.rightSpellSlotID == item.itemID)
                loadedRightSpell = item as SpellData;
        }
        foreach(var item in GetItemDataBase()){
            if(item != null && _data.dashSpellSlotID == item.itemID)
                loadedDashSpell = item as SpellData;
        }
        foreach(var item in GetItemDataBase()){
            if(item != null && _data.qSpellSlotID == item.itemID)
                loadedQSpell = item as SpellData;
        }
        foreach(var item in GetItemDataBase()){
            if(item != null && _data.eSpellSlotID == item.itemID)
                loadedESpell = item as SpellData;
        }

    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equipmentId.Clear();
        _data.leftSpellSlotID = "";
        _data.rightSpellSlotID = "";
        _data.dashSpellSlotID = "";
        _data.qSpellSlotID = "";
        _data.eSpellSlotID = "";

        foreach (KeyValuePair<ItemData, InventoryItem> pair in inventoryDictianory)
        {
            _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
        }

        foreach (KeyValuePair<ItemData, InventoryItem> pair in stashDictianory)
        {
            _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
        }

        foreach (KeyValuePair<EquipmentItemData, InventoryItem> pair in equipmentDictionary)
        {
            _data.equipmentId.Add(pair.Key.itemID);
        }

        foreach (KeyValuePair<SpellData, InventoryItem> pair in spellDictionary){
            _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
        }

        if(leftClickSlot.item != null && leftClickSlot.item.data != null)
            _data.leftSpellSlotID = leftClickSlot.item.data.itemID;

        if(rightClickSlot.item != null && rightClickSlot.item.data != null)
            _data.rightSpellSlotID = rightClickSlot.item.data.itemID;

        if(dashSlot.item != null && dashSlot.item.data != null)
            _data.dashSpellSlotID = dashSlot.item.data.itemID;

        if(qSlot.item != null && qSlot.item.data != null)
            _data.qSpellSlotID = qSlot.item.data.itemID;

        if(eSlot.item != null && eSlot.item.data != null)
            _data.eSpellSlotID = eSlot.item.data.itemID;
    }

    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Items" });

        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }

        return itemDataBase;
    }

    /*public void UseFlask()
    {
        EquipmentItemData currentFlask = GetEquipment(EquipmentType.Flask);

        if (currentFlask == null)
            return;

        bool canUseFlask = Time.time > lastTimeUsedFlask + flaskCooldown;

        if (canUseFlask)
        {
            flaskCooldown = currentFlask.itemCooldown;
            currentFlask.Effect(null);
            lastTimeUsedFlask = Time.time;
        }
        else
            Debug.Log("Flask on cooldown;");
    }

    public bool CanUseArmor()
    {
        EquipmentItemData currentArmor = GetEquipment(EquipmentType.Armor);

        if (Time.time > lastTimeUsedArmor + armorCooldown)
        {
            armorCooldown = currentArmor.itemCooldown;
            lastTimeUsedArmor = Time.time;
            return true;
        }

        Debug.Log("Armor on cooldown");
        return false;
    }*/
}
