using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public Image healthBar;
    public Image staminaBar;
    public TextMeshProUGUI currencyText;
    public CharacterStats playerStats => PlayerManager.instance.player.stats;// Reference to your player stats script


    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp01(playerStats.currentHp / (float)playerStats.maxHp.GetValue());
        staminaBar.fillAmount = playerStats.currentStamina / playerStats.maxStamina.GetValue();
        currencyText.text = PlayerManager.instance.GetCurrency().ToString();
    }
}
