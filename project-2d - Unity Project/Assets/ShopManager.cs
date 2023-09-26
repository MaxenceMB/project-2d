using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour

{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject itemSlotShopPrefab;
    [SerializeField] Item[] itemsOnSale;
    [SerializeField] GameObject player;
    [SerializeField] GameObject selectedSlotShopPrefab;
    private Vector3 positionItems;
    private Vector3 positionSelection;
    private GameObject selectedSlot;
    private int currentSlot;
    private Item selectedItem;
    private GameObject selectedSlotBorder;


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

        if (Input.GetKeyDown(KeyCode.V)) {
            currentSlot = 0;
            selectedItem = itemsOnSale[0];
            SetSpeechTextWithItem();
            if (canvas.activeSelf) {
                canvas.SetActive(false);
                Destroy(this.selectedSlotBorder);
            } else {
                canvas.SetActive(true);
                 positionSelection = new Vector3(963, 530);
                this.selectedSlotBorder = GameObject.Instantiate(selectedSlotShopPrefab, positionSelection, Quaternion.identity, this.canvas.transform);
            }
        }

        if (canvas.activeSelf) {
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && currentSlot < itemsOnSale.Length-1) {
                currentSlot++;
                selectedItem = itemsOnSale[currentSlot];
                SetSpeechTextWithItem();
                selectedSlotBorder.transform.position += new Vector3(0,-130,0);
            }
            if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && currentSlot > 0) {
                currentSlot--;
                selectedItem = itemsOnSale[currentSlot];
                SetSpeechTextWithItem();
                selectedSlotBorder.transform.position += new Vector3(0,130,0);
            }
            if (Input.GetKeyDown(KeyCode.C)){
                if (GameObject.Find("PouchManager").GetComponent<PouchManager>().CanAfford(selectedItem.price)) {
                    GameObject.Find("InventoryManager").GetComponent<InventoryManager>().AddItem(selectedItem);
                    GameObject.Find("PouchManager").GetComponent<PouchManager>().LoseMoney(selectedItem.price);
                    canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("Thanks for your purchase!");
                } else {
                    canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText("oh the hobo IJBOL");
                    StartCoroutine(GameObject.Find("Player").GetComponentInChildren<PouchManager>().ShakePouch());
                }
            }
            
        }
    }

    void SetSpeechTextWithItem() {
        canvas.transform.Find("SpeechShop").GetComponent<TMP_Text>().SetText(selectedItem.name+
        "? That will be "+selectedItem.price+"€");
    }
}
