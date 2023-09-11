using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickingUpItem : MonoBehaviour
{
    [SerializeField] public InventoryManager playerInventory;
    public TMP_Text inventoryFullText;
    public float textDuration = 2f;

    public void Awake(){
        inventoryFullText.text = "Inventory Full !";
        inventoryFullText.gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PickableItem")){
            if (playerInventory.AddItem(other.gameObject.GetComponent<ItemDisplayer>().item)){
                Destroy(other.gameObject);
            } else {
                StartCoroutine(ShowInventoryFullText());
            }
        }
    }

    IEnumerator ShowInventoryFullText(){
        inventoryFullText.gameObject.SetActive(true);
        float remainingTextTime = textDuration;
        yield return new WaitForSeconds(textDuration);
        inventoryFullText.gameObject.SetActive(false);
    } 

}
