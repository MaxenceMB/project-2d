using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public static int MAX_STACK_SIZE = 2;

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public int selectedSlot = -1;

    private void Start() {
        Debug.Log(inventorySlots.Length);
        ChangeSelectedSlot(1);
    }

    public void ChangeSelectedSlot(int newSelectedSlot){
        if (selectedSlot >= 0 && selectedSlot < inventorySlots.Length){
            inventorySlots[selectedSlot].Deselect();
            inventorySlots[newSelectedSlot].Select();
            selectedSlot = newSelectedSlot;
        }
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
}
