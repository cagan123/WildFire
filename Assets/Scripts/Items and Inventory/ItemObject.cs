using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
     [SerializeField] private ItemData itemData;

    private void OnValidate(){
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item Object -" + itemData.ItemName;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.GetComponentInParent<Player>() != null){
            Inventory.instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
