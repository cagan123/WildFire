using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTooltipUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI spellNameText;
    [SerializeField] private TMPro.TextMeshProUGUI spellDescriptionText;
    [SerializeField] private TMPro.TextMeshProUGUI spellStatText;


    public void ShowToolTip(SpellData item)
    {
        if (item == null)
            return;

        gameObject.SetActive(true);
        spellNameText.text = item.itemName;
        spellDescriptionText.text = item.GetDescription();
        spellStatText.text = item.damage.ToString();
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
