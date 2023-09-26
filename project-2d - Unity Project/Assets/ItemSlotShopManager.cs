using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotShopManager : MonoBehaviour

{

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
