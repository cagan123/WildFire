using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Spell")]
public class SpellData : ItemData
{
    public int StaminaCost; // Stamina cost of the spell
    public int damage; // Damage of the spell
    public int poise; // Poise of the spell
    public string statusEffects; // Status effects of the spell
    public GameObject spellPrefab; // Prefab of the spell

}
