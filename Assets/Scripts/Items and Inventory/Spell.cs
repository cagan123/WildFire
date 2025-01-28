using UnityEngine;

[CreateAssetMenu(menuName = "SpellData")]
public class Spell : ScriptableObject
{
    public string spellName; // Name of the spell
    public string description; // Description of the spell
    public int damage; // Damage of the spell
    public int poise; // Poise of the spell
    public string statusEffects; // Status effects of the spell
    public Sprite Icon; // Icon of the spell
    public GameObject spellPrefab; // Prefab of the spell

    public Spell(string name, Sprite icon)
    {
        spellName = name;
        Icon = icon;
    }
}
