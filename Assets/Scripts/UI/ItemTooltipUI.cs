using System.Collections;
using TMPro;
using UnityEngine;

public class ItemTooltipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private int defaultFontSize = 32;

    public void ShowToolTip(EquipmentItemData item)
    {
        if (item == null)
            return;

        itemNameText.text = item.itemName;
        itemTypeText.text = item.equipmentType.ToString();
        itemDescription.text = item.GetDescription();
        gameObject.SetActive(true);
    }
    public void ShowToolTip(SpellData item)
    {
        if (item == null)
            return;

        itemNameText.text = item.itemName;
        itemTypeText.text = item.damage.ToString() + " Damage" + item.poise.ToString() + " Poise Damage" + item.statusEffects;
        itemDescription.text = item.GetDescription();
        gameObject.SetActive(true);

    }

    public void HideToolTip() 
    {
        itemNameText.fontSize = defaultFontSize;
        gameObject.SetActive(false);
    }
}
