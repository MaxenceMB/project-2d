using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0){
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        } else {
            InventoryItem currentSlotItem = transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

            //currentSlotItem.parentAfterDrag = inventoryItem.transform;
            currentSlotItem.transform.SetParent(inventoryItem.parentBeforeDrag);
            inventoryItem.parentAfterDrag = transform;
        }
    }

}
