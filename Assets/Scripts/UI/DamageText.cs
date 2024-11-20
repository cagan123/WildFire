using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    void Start()
    {
        if (damageText == null)
            damageText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowDamageText(int damage)
    {
        damageText.text = damage.ToString();
        
    }
    public void DestroyText(){
        Destroy(gameObject); 
    }
}
