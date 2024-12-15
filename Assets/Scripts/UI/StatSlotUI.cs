using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatSlotUI : MonoBehaviour
{
    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;
    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;


        if(statNameText != null)
            statNameText.text = statName;
    }
    void Start()
    {
        UpdateStatValueUI();
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
}
