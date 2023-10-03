using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public static int MAX_STACK_SIZE = 2;

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public Transform player;

    public int selectedSlot = -1;


    /// <summary>
    /// On start, set the first slot as selected
    /// </summary>
    private void Start() {
        ChangeSelectedSlot(0);
    }

    public void Update(){
        // Select an inventory slot with the alphanumerical keys (1-5)
        if(Input.inputString != null){
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && (number >= 0 && number <= 5)){
                ChangeSelectedSlot(number - 1);
                selectedSlot = number - 1;
            }
        }

        // Scroll trough toolbar's slots with the mouse scrollwheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f){
            ChangeSelectedSlot(Mathf.Max(selectedSlot - 1, 0));
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f){
            ChangeSelectedSlot(Mathf.Min(selectedSlot + 1, 4));
        }

        // Use item in selected slot
        if (Input.GetKeyUp(KeyCode.F)){
            UseSelectedItem();
        }
        
        ShowItemInPlayerHands();
    }


    /// <summary>
    /// Changes the selected slot to the given slot
    /// </summary>
    /// <param name="newSelectedSlot">  int: the new selected slot index </param>
    public void ChangeSelectedSlot(int newSelectedSlot){
        if (selectedSlot >= 0){
            inventorySlots[selectedSlot].Deselect();
        }
        if (newSelectedSlot >= 0 && newSelectedSlot < inventorySlots.Length){
            inventorySlots[newSelectedSlot].Select();
            selectedSlot = newSelectedSlot;
        }
    }


    /// <summary>
    /// Adds an item to the first free slot or to the first stack that can hold more of the item
    /// </summary>
    /// <param name="item"> Item: the item to add </param>
    /// <returns>           bool: TRUE if the item has been added, FALSE else </returns>
    public bool AddItem(Item item){
        for (int i = 0; i < inventorySlots.Length; i++){
            InventoryItem itemInSlot = inventorySlots[i].GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null
                && itemInSlot.item == item
                && itemInSlot.quantity < MAX_STACK_SIZE
                && itemInSlot.item.stackable){
                itemInSlot.quantity++;
                itemInSlot.RefreshText();
                return true;
            } else if (itemInSlot == null) {
                SpawnNewItem(item, inventorySlots[i]);
                return true;
            } 
        }
        return false;
    }


    /// <summary>
    /// Spawns the given item in the given slot
    /// </summary>
    /// <param name="item"> Item: item to spawn </param>
    /// <param name="slot"> Slot: slot to spawn in </param>
    void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }


    /// <summary>
    /// Returns the selected slot's item or null if no item is present in that slot
    /// </summary>
    /// <returns>   Item: the item in the selected slot </returns>
    public Item GetSelectedItem(){
        InventoryItem itemInSlot = inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null){
            return itemInSlot.item;
        }
        return null;
    }


    /// <summary>
    /// Uses the item in the select slot if it is a consumable
    /// </summary>
    public void UseSelectedItem(){
        InventoryItem itemInSlot = inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null){
            if (itemInSlot.item is ConsumableItem){
                itemInSlot.quantity--;
                if (itemInSlot.quantity <= 0){
                    Destroy(itemInSlot.gameObject);
                } else {
                    itemInSlot.RefreshText();
                }
            }
        }
    }


    /// <summary>
    /// Displays the item in the selected slot on the player sprite
    /// </summary>
    public void ShowItemInPlayerHands(){
        Item item = GetSelectedItem();
        if (item is Weapon){
            Weapon weaponItem = (Weapon) item;
            player.GetChild(0).gameObject.SetActive(true);
            player.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = weaponItem.activeSprite;
        } else {
            player.GetChild(0).gameObject.SetActive(false);
        }
    }

}
