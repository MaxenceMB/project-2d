using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// manages a slot for an item on sale in a shop
/// </summary>

public class ItemSlotShopManager : MonoBehaviour

{

    /// <summary>
    /// sets the image and the name of the item on sale in the current shop slot
    /// </summary>
    /// <param name="item"> the item which's informations should be displayed </param>
    public void setItemSlotShop(Item item) {
        transform.GetChild(2).GetComponentInChildren<Image>().sprite = item.icon;
        transform.GetChild(3).GetComponent<TMP_Text>().SetText(item.itemName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
