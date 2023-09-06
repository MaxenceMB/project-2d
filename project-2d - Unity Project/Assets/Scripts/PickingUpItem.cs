using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUpItem : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PickableItem")){
            playerInventory.AddItem(other.gameObject.GetComponent<ItemDisplayer>().GetItem(), 1);
            Debug.Log(playerInventory.ToString());
            Destroy(other.gameObject);
        }
    }

}
