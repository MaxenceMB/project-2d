using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    
    public void AddItem(Item item){
        for (int i = 0; i < inventorySlots.Length; i++){
            if (inventorySlots[i].GetComponent<InventoryItem>() == null){
                SpawnNewItem(item, inventorySlots[i]);
                return;
            }
        }
    }

    void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
}
