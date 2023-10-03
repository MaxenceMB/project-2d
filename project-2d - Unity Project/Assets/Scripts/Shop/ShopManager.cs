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
    /// the shop's canvas
    [SerializeField] GameObject canvas;

    /// the player
    [SerializeField] GameObject player;

    /// the prefab corresponding to an item slot in the shop
    [SerializeField] GameObject itemSlotShopPrefab;
    
    /// the prefab corresponding to the different shop options
    [SerializeField] GameObject shopOptionsPrefab;

    [SerializeField] GameObject inventorySlotPrefab;

    /// array containing all the items on sale in this shop
    [SerializeField] Item[] itemsOnSale;

    /// array containing all the options available for the shop
    private string[] shopOptions = {"Buy", "Sell", "Leave", "Selection"};

    /// the prefab corresponding to the cursor indicating the selected item
    [SerializeField] GameObject cursorItemShopPrefab;

    /// the prefab corresponding to the cursor indicating the selected option
    [SerializeField] GameObject cursorOptionShopPrefab;

    /// the index indicating the currently selected item/option
    private int currentSlot;

    /// the item currently selected
    private Item selectedItem;

    /// the option currently selected
   private string selectedShopOption;

    /// the vector used to place the different slots
    private Vector3 slotsPosition;

    /// the vector corresponding to the cursor indicating the currently selected item
    private Vector3 cursorPosition;

    // the cursor indicating the currently selected option
    private GameObject optionsCursor;

    // the cursor indicating the currently selected item
    private GameObject itemsCursor;

    /// boolean indicating whether the player can reach and interact
    /// with the shop depending on if they are in the shop's interaction area
    private bool reachable;

    // boolean indicating whether the player bought something or not
    private bool purchaseDone;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        currentSlot = 0;
        selectedShopOption = "Selection";
        displaySelectedOption();
        SetSpeechTextWithOption(shopOptions[currentSlot]);
    }

    // Update is called once per frame
    void Update()
    {   

        //if E is pressed while the shop is reachable and unopened, the shop opens
        if (reachable && Input.GetKeyDown(KeyCode.E) && !canvas.activeSelf) {
            openShop();
        }

        //if the shop is open
        if (canvas.activeSelf) {

            //if the current shop option is selection
            if (this.selectedShopOption == "Selection") {
                if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && currentSlot < 2) {
                    currentSlot++;
                    optionsCursor.transform.position += new Vector3(0,-130, 0);
                    SetSpeechTextWithOption(shopOptions[currentSlot]);
                }
                if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && currentSlot > 0) {
                    currentSlot--;
                    optionsCursor.transform.position += new Vector3(0,130,0);
                    SetSpeechTextWithOption(shopOptions[currentSlot]);
                }
                //if C is pressed, the shop option is selected
                if (Input.GetKeyDown(KeyCode.C) && currentSlot < 3) {
                    selectedShopOption = this.shopOptions[currentSlot];
                    currentSlot = 0;
                    displaySelectedOption();
                }
                //if X is pressed, the shop is closed
                if ((Input.GetKeyDown(KeyCode.X))) {
                    closeShop();
                }

            //if the current shop option is buy
            } else if (this.selectedShopOption == "Buy") {
                if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && currentSlot < itemsOnSale.Length-1) {
                    currentSlot++;
                    itemsCursor.transform.position += new Vector3(0,-130,0);
                    this.selectedItem = itemsOnSale[currentSlot];
                    SetSpeechTextWithItem();
                }
                if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && currentSlot > 0) {
                    currentSlot--;
                    itemsCursor.transform.position += new Vector3(0,130,0);
                    this.selectedItem = itemsOnSale[currentSlot];
                    SetSpeechTextWithItem();
                }
                //if C is pressed, buys the current item 
                if (Input.GetKeyDown(KeyCode.C)){
                    if (GameObject.Find("PouchManager").GetComponent<PouchManager>().CanAfford(selectedItem.price)) {
                        purchaseDone = true;
                        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().AddItem(selectedItem);
                        GameObject.Find("PouchManager").GetComponent<PouchManager>().LoseMoney(selectedItem.price);
                        canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("Thanks for your purchase!");
                    } else {
                        canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("It seems you don't have enough to buy this. Maybe you should sell me something?");
                    }
                }
                //if X is pressed, goes back to the option menu
                if (Input.GetKeyDown(KeyCode.X)){
                    currentSlot = 0;
                    selectedShopOption = "Selection";
                    displaySelectedOption();
                    SetSpeechTextWithOption(shopOptions[currentSlot]);
                }
            }

        }
    }

    /// <summary>
    /// displays the needed content for the selected option
    /// </summary>
    private void displaySelectedOption() {
        destroyUselessComponents(); 
        switch (this.selectedShopOption) {
            case "Selection":
                slotsPosition = new Vector3(760, 550);
                cursorPosition = new Vector3(760, 680);
                GameObject shopOptions = Instantiate(this.shopOptionsPrefab, slotsPosition, Quaternion.identity, this.canvas.transform);
                this.optionsCursor = Instantiate(this.cursorOptionShopPrefab, cursorPosition, Quaternion.identity, this.canvas.transform);
                break;
            case "Buy":
                slotsPosition = new Vector3(590, 720);
                cursorPosition = new Vector3(963,530);
                this.itemsCursor = Instantiate(cursorItemShopPrefab, this.cursorPosition, Quaternion.identity, this.canvas.transform);
                foreach(Item item in itemsOnSale) {
                    GameObject itemSlot = GameObject.Instantiate(itemSlotShopPrefab, this.slotsPosition, Quaternion.identity, this.canvas.transform);
                    itemSlot.GetComponent<ItemSlotShopManager>().setItemSlotShop(item);
                    this.slotsPosition += new Vector3(0, -130);
                }
                selectedItem = itemsOnSale[0];
                SetSpeechTextWithItem();
                break; 
            case "Leave":
                closeShop();
                break;
        }
    }

    /// <summary>
    /// destroys all the components of the canvas related to a specific option
    /// </summary>
    private void destroyUselessComponents() {
        for (int i = 0; i<this.canvas.transform.childCount; i++) {
            string nom = this.canvas.transform.GetChild(i).name;
            if (!(nom == "BackgroundShop" || nom == "SpeechBubbleShop" || nom == "SpeechShop")) {
                Destroy(canvas.transform.GetChild(i).GameObject());
            }
        }
    }

    /// <summary>
    /// set the shop owner's bubble speech according to the currently selected item
    /// </summary>
    private void SetSpeechTextWithItem() {
        canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText(selectedItem.name+
        "? That will be "+selectedItem.price+"€.");
    }


    /// <summary>
    /// sets the shop owner's bubble speech according to the current shop option 
    /// </summary>
    /// <param name="option"></param>
    private void SetSpeechTextWithOption(string option) {

        switch (option) {
            case "Selection" : 
                canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("Welcome to our shop! We have everything you need.");
                break;
            case "Sell" :
                canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("Want to sell something? Let's see what you have to offer.");
                break;
            case "Buy" :
                canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("I have a lot of interesting things on sale right now! Come take a look.");
                break;
            case "Leave" :
                //depending on whether a purchase in the shop was made or not, the shop owner won't have the same 
                //reaction to the player trying to leave the shop 
                if (purchaseDone) {
                    canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("Leaving already? Thanks for coming by!");
                } else {
                    canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("Huh, so just window shopping?");
                }
                break;
        }

    }

    /// <summary>
    /// closes the shop and resets it to its default state
    /// </summary>
    private void closeShop() {
        canvas.SetActive(false);
        currentSlot = 0;
        this.selectedItem = this.itemsOnSale[0];
        this.selectedShopOption = "Selection";
        purchaseDone = false;
        SetSpeechTextWithOption("Selection");
        displaySelectedOption();
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }

    /// <summary>
    /// opens the shop
    /// </summary>
    private void openShop() {
        canvas.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
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