using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;
    [SerializeField] private Vector2 velocity;

    private void OnValidate(){
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item Object -" + itemData.ItemName;
    }
    public void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            rb.AddForce(velocity, ForceMode2D.Impulse);        
        }
    }

    public void PickUpItem()
    {
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
