using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpellTooltipUI : MonoBehaviour
{
    public TextMeshProUGUI spellNameText;
    public TextMeshProUGUI spellDescriptionText;
    public TextMeshProUGUI spellStatsText;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        HideTooltip();
    }

    public void ShowTooltip(Spell spell, Vector2 position)
    {
        spellNameText.text = spell.spellName;
        spellDescriptionText.text = spell.description;
        spellStatsText.text = $"Damage: {spell.damage}\nEffects: {spell.statusEffects}";

        // Position the tooltip
        //rectTransform.anchoredPosition = position;
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
