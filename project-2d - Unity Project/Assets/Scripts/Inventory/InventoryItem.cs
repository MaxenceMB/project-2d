using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Image image;
    public TMP_Text quantityText;

    [HideInInspector] public Item item;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int quantity = 1;

    private void Start() {
        InitialiseItem(item);
    }


    /// <summary>
    /// Initialises the item by setting it's sprite to the given item one
    /// </summary>
    /// <param name="newItem"></param>
    public void InitialiseItem(Item newItem){
        item = newItem;
        image.sprite = newItem.icon;
        RefreshText();
    }


    /// <summary>
    /// Refreshes the stack size text and displays it only if the stack size is greater than one
    /// </summary>
    public void RefreshText(){
        quantityText.text = quantity.ToString();
        bool showQuantity = quantity > 1;
        quantityText.gameObject.SetActive(showQuantity);
    }


    /// <summary>
    /// When the player beggins dragging the item, disables the item's raycast so that when the player releases
    /// the item, the object right behind the cursor isn't the item itself, which would never trigger any 'OnDrop'
    /// method on any other object.
    /// Saves the item's current parent.
    /// </summary>
    /// <param name="eventData">    PointerEventData: the mouse drag event </param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }


    /// <summary>
    /// While the item is being dragged, sets it's position the the mouse position
    /// </summary>
    /// <param name="eventData">    PointerEventData: the mouse drag event </param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }


    /// <summary>
    /// When the player drops the item, enables back the item's raycast and sets the
    /// item's parent to slot it has been dropped into, or keeps the old parent if 
    /// its has been dropped anywhere else
    /// </summary>
    /// <param name="eventData">    PointerEventData: the mouse drag event </param>
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

}
