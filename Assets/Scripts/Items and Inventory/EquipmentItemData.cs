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
}
