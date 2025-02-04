using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SpellEquipmentSlotUI : ItemSlotUI
{
    [SerializeField] public KeyCode keycode;
    private void Update(){
        if(item == null || item.data == null)
            return;
        Debug.Log(keycode + item.data.name);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if(item == null || item.data == null)
            return;

        if(keycode == KeyCode.Mouse0){
            Inventory.instance.RemoveFromLeftClickSlot();
        }
        if(keycode == KeyCode.Mouse1){
            Inventory.instance.RemoveFromRightClickSlot();
        }
        if(keycode == KeyCode.Q){
            Inventory.instance.RemoveFromQSlot();
        }
        if(keycode == KeyCode.E){
            Inventory.instance.RemoveFromESlot();
        }
        if(keycode == KeyCode.Space){
            Inventory.instance.RemoveFromDashSlot();
        }
        CleanUpSlot();
    }
}
