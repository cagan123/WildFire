using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image healthBar;
    public Image staminaBar;
    public CharacterStats playerStats => PlayerManager.instance.player.stats;// Reference to your player stats script


    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp01(playerStats.currentHp / (float)playerStats.maxHp.GetValue());
        staminaBar.fillAmount = playerStats.currentStamina / playerStats.maxStamina.GetValue();
    }
}

