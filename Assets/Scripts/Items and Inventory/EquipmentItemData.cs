using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType{
    Weapon,
    Armor,
    Amulet,
    Flask
}
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class EquipmentItemData : ItemData
{
    public EquipmentType equipmentType;

    public int damage;
    public int vitality;
    public int inteligence;
    public int stamina;

    [Header("Craft Requirements")]
    public List<InventoryItem> craftingMaterials;
    public void AddModifiers(){
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        FireSpiritStats fireSpiritStats = FireSpiritManager.instance.fireSpirit.GetComponent<FireSpiritStats>();
        
        fireSpiritStats.damage.AddModifier(damage);
        playerStats.maxHp.AddModifier(vitality);
        playerStats.maxStamina.AddModifier(stamina);
    }
    public void RemoveModifiers(){
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        FireSpiritStats fireSpiritStats = FireSpiritManager.instance.fireSpirit.GetComponent<FireSpiritStats>();
        
        fireSpiritStats.damage.RemoveModifier(damage);
        playerStats.maxHp.RemoveModifier(vitality);
        playerStats.maxStamina.RemoveModifier(stamina);
    }
}
