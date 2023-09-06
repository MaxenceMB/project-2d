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

    private Sprite icon;

    void Start(){
        icon = item.getIcon();
        spriteRenderer.sprite = icon;
        spriteRenderer.enabled = true;
    }

}
