using TMPro;
using UnityEngine;

public class ExlamationText : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    void Start()
    {
        if (damageText == null)
            damageText = GetComponentInChildren<TextMeshProUGUI>();
          
        damageText.fontStyle = FontStyles.Bold; 
    }
    public void DestroyText(){
        Destroy(gameObject); 
    }
}
