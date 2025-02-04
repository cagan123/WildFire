using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SpellKeyUI : MonoBehaviour
{
    public SpellData currentSpell;
    public void ReturnButtonPress(Button button){
        if(button.name == "Left_Mouse_Button" && currentSpell != null){
            Inventory.instance.AddToLeftClickSlot(currentSpell);
        }   
        if(button.name == "Right_Mouse_Button" && currentSpell != null){
            Inventory.instance.AddToRightClickSlot(currentSpell);
        }
        if(button.name == "Dash_Button" && currentSpell != null){
            Inventory.instance.AddToDashSlot(currentSpell);
        }
        if(button.name == "Q_Button" && currentSpell != null){
            Inventory.instance.AddToQSlot(currentSpell);
        }
        if(button.name == "E_Button" && currentSpell != null){
            Inventory.instance.AddToESlot(currentSpell);
        }
    }
    public void ShowToolTip(SpellData item)
    {
        if (item == null)
            return;
        gameObject.SetActive(true);
        transform.position = Input.mousePosition;
    }

    public void HideToolTip() 
    {
        currentSpell = null;
        gameObject.SetActive(false);
    }
}
