using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUpItem : MonoBehaviour
{
    [SerializeField] public InventoryManager playerInventory;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PickableItem")){
            playerInventory.AddItem(other.gameObject.GetComponent<ItemDisplayer>().GetItem());
            Destroy(other.gameObject);
        }
    }

}
