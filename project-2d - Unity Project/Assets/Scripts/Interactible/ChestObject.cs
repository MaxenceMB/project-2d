using UnityEngine;

[CreateAssetMenu(fileName = "New_Chest", menuName = "Interactibles/New Chest")]
public class ChestObject : ScriptableObject {
    
    [Header("Chest Object")]
    [SerializeField] private string title;
    [SerializeField] private bool alreadyOpened = false;

    [Header("Chest ")]
    [SerializeField] private Item[] item;

    public void Interact() {
        if(alreadyOpened) { // Ending condition
            ScreenTexts.HideText();
            PlayerInteractions.state = InteractStates.End;
        } else {
            ScreenTexts.ShowText("- chest opened -", 50, TextPos.CENTER, true);
            alreadyOpened = true;
        }
    }

}
