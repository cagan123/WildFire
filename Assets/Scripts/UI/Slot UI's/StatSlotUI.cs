using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatSlotUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private UI UI;
    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;

    [TextArea]
    [SerializeField] private string statDescription;
    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;


        if(statNameText != null)
            statNameText.text = statName;
    }
    void Start()
    {
        UpdateStatValueUI();
        UI = GetComponentInParent<UI>();
    }
    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        FireSpiritStats fireSpiritStats = FireSpiritManager.instance.fireSpirit.GetComponent<FireSpiritStats>();

        if(playerStats != null){
            statValueText.text = playerStats.GetStat(statType).GetValue().ToString();
            if(statType == StatType.damage){
                statValueText.text = fireSpiritStats.GetStat(statType).GetValue().ToString();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UI.statTooltip.ShowStatTooltip(statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.statTooltip.HideStatTooltip();
    }
}
