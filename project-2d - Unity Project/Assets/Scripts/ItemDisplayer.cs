using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDisplayer : MonoBehaviour
{
    public Item item;
    public SpriteRenderer spriteRenderer;

    public string itemName;
    public string description;
    public int quantity;

    private Sprite icon;

    void Start(){
        icon = item.getIcon();
        spriteRenderer.sprite = icon;
        this.quantity = item.getQuantity();
    }

    public Item GetItem(){
        return item;
    }
}
