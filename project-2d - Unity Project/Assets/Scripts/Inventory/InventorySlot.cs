using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler {

    public Image image;
    public Color selectedColor, deselectedColor;


    /// <summary>
    /// On start, the slot is deselected so that every slots in the inventory are deselected
    /// </summary>
    private void Awake(){
        Deselect();
    }


    /// <summary>
    /// Highlights the slot when it is selected
    /// </summary>
    public void Select(){
        image.color = selectedColor;
    }


    /// <summary>
    /// Reverts back to a normal slot when the another slot is selected
    /// </summary>
    public void Deselect(){
        image.color = deselectedColor;
    }


    /// <summary>
    /// Uppon dragging and dropping an item on this slot, changes the item's parent to this slot
    /// If an item was already present on this slot, changes this item's parent to the dropped item's old parent
    /// </summary>
    /// <param name="eventData">    PointerEventData: the mouse drop event </param>
    public void OnDrop(PointerEventData eventData){
        if (transform.childCount == 0){
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        } else {
            InventoryItem currentSlotItem = transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            currentSlotItem.transform.SetParent(inventoryItem.parentAfterDrag);
            inventoryItem.parentAfterDrag = transform;
        }
    }

}
