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

    private int descriptionLength;
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
    public override string GetDescription()
    {
        sb.Length = 0;
        descriptionLength = 0;

        AddItemDescription(damage, "Damage");
        AddItemDescription(vitality, "Vitality");
        AddItemDescription(inteligence, "Inteligence");
        AddItemDescription(stamina, "Stamina");

        if (descriptionLength < 4)
        {
            for (int i = 0; i < 4 - descriptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }
        return sb.ToString();
    }
    private void AddItemDescription(int _value, string _name){
        if(_value != 0){
            if(sb.Length > 0){
                sb.AppendLine();
            }
            if(_value > 0){
                sb.Append("+ " + _value + " " + _name);
            }
            descriptionLength ++;
        }
    }
}
