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

    public void InitialiseItem(Item newItem){
        item = newItem;
        image.sprite = newItem.icon;
        RefreshText();
    }

    public void RefreshText(){
        quantityText.text = quantity.ToString();
        bool showQuantity = quantity > 1;
        quantityText.gameObject.SetActive(showQuantity);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

}
