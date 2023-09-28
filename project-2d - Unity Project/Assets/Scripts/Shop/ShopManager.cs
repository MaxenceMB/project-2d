using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// manages a shop
/// </summary>

public class ShopManager : MonoBehaviour

{
    /// <summary>
    /// the shop's UI canvas
    /// </summary>
    [SerializeField] GameObject canvas;

    /// <summary>
    /// the prefab corresponding the item slots in the shop
    /// </summary>
    [SerializeField] GameObject itemSlotShopPrefab;

    /// <summary>
    /// table of all the items on sale in this shop
    /// </summary>
    [SerializeField] Item[] itemsOnSale;

    /// <summary>
    /// the player
    /// </summary>
    [SerializeField] GameObject player;

    /// <summary>
    /// the prefab corresponding to the currently selected item's prefab
    /// </summary>
    [SerializeField] GameObject selectedSlotShopPrefab;

    /// <summary>
    /// the vector used to place the items slots's positions
    /// </summary>
    private Vector3 positionItems;

    /// <summary>
    /// the vector corresponding to the cursor indicating the
    /// currently selected item
    /// </summary>
    private Vector3 positionSelection;

    /// <summary>
    /// the index of the item currently selected in the table of itemsOnSale
    /// </summary>
    private int currentSlot;

    /// <summary>
    /// the item currently selected
    /// </summary>
    private Item selectedItem;

    /// <summary>
    /// the cursor used to indicate which item slot is selected
    /// </summary>
    private GameObject selectedSlotBorder;

    /// <summary>
    /// boolean to indicate whether the player can reach and interact
    /// with the shop depending on if they are in the shop's interaction area
    /// </summary>
    private bool reachable;


    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        positionItems = new Vector3(590, 720);
        ;
        foreach(Item item in itemsOnSale) {
            GameObject itemSlot = GameObject.Instantiate(itemSlotShopPrefab, this.positionItems, Quaternion.identity, this.canvas.transform);
            itemSlot.GetComponent<ItemSlotShopManager>().setItemSlotShop(item);
            this.positionItems += new Vector3(0, -130);
        }
        positionItems = new Vector3(590, 720);
        selectedItem = itemsOnSale[0];
        SetSpeechTextWithItem();
    }

    // Update is called once per frame
    void Update()
    {   
        if (reachable) {

            //if E is pressed when the player can reach the shop, closes
            //or opens the shop depending on whether it's already open or not
            if (Input.GetKeyDown(KeyCode.E)) {
                //selects the first item as the currently selected one
                currentSlot = 0;
                selectedItem = itemsOnSale[0];
                SetSpeechTextWithItem();
                if (canvas.activeSelf) {
                    //if the shop is open, closes it and allows the player to move again
                    canvas.SetActive(false);
                    GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
                    Destroy(this.selectedSlotBorder);
                } else {
                    //if the shop is closed, opens it and prevents the player from moving
                    canvas.SetActive(true);
                    GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
                    positionSelection = new Vector3(963, 530);
                    this.selectedSlotBorder = GameObject.Instantiate(selectedSlotShopPrefab, positionSelection, Quaternion.identity, this.canvas.transform);
                }
            }
        }

        if (canvas.activeSelf) {
            //selects the item below the currently selected one
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && currentSlot < itemsOnSale.Length-1) {
                currentSlot++;
                selectedItem = itemsOnSale[currentSlot];
                SetSpeechTextWithItem();
                selectedSlotBorder.transform.position += new Vector3(0,-130,0);
            }
            //selects the item above the currently selected one 
            if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && currentSlot > 0) {
                currentSlot--;
                selectedItem = itemsOnSale[currentSlot];
                SetSpeechTextWithItem();
                selectedSlotBorder.transform.position += new Vector3(0,130,0);
            }
            //buys the currently selected item
            if (Input.GetKeyDown(KeyCode.C)){
                if (GameObject.Find("PouchManager").GetComponent<PouchManager>().CanAfford(selectedItem.price)) {
                    GameObject.Find("InventoryManager").GetComponent<InventoryManager>().AddItem(selectedItem);
                    GameObject.Find("PouchManager").GetComponent<PouchManager>().LoseMoney(selectedItem.price);
                    canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("Thanks for your purchase!");
                } else {
                    canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("oh the hobo IJBOL");
                }
            }
            
        }
    }

    /// <summary>
    /// changes the shop owner's bubble speech according to the currently selected item
    /// </summary>
    void SetSpeechTextWithItem() {
        canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText(selectedItem.name+
        "? That will be "+selectedItem.price+"€");
    }

    /// <summary>
    /// assigns true to the reachable variable to indicate the shop is reachable
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        reachable = true;
    }

    /// <summary>
    /// assigns false to the reachable variable to indicate the shop is unreachable
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other) {
        reachable = false;
    }
}
