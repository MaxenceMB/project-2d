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

    void Start(){
        spriteRenderer.sprite = item.icon;
    }

    public Item GetItem(){
        return item;
    }
}
