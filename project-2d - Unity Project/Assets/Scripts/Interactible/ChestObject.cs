using UnityEngine;

[CreateAssetMenu(fileName = "New_Chest", menuName = "Interactibles/Chest")]
public class ChestObject : ScriptableObject {
    
    [Header("Chest Object")]
    [SerializeField] private string title;
    [SerializeField] private bool alreadyOpened;

    [Header("Chest ")]
    [SerializeField] private Item[] item;

    public void Interact() {
        Debug.Log("CHEST OPENED");
        this.alreadyOpened = true;
    }

}
