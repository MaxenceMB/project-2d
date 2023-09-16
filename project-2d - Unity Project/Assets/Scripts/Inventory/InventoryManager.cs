using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public static int MAX_STACK_SIZE = 2;

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public Transform player;

    public int selectedSlot = -1;

    private void Start() {
        ChangeSelectedSlot(0);
    }

    public void ChangeSelectedSlot(int newSelectedSlot){
        if (selectedSlot >= 0){
            inventorySlots[selectedSlot].Deselect();
        }
        if (newSelectedSlot >= 0 && newSelectedSlot < inventorySlots.Length){
            inventorySlots[newSelectedSlot].Select();
            selectedSlot = newSelectedSlot;
        }
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

    void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(){
        InventoryItem itemInSlot = inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null){
            return itemInSlot.item;
        }
        return null;
    }

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

    public void ShowItemInPlayerHands(){
        Item item = GetSelectedItem();
        if (item is WeaponItem){
            WeaponItem weaponItem = (WeaponItem) item;
            player.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = weaponItem.activeSprite;
        } else {
            player.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
