using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New_PNJ", menuName = "Interactibles/PNJ/New PNJ")]
public class PNJObject : ScriptableObject {
    
    [Header("PNJ Object")]
    [SerializeField] private string nickname;
    [SerializeField] private bool alreadyTalked;

    [Header("Dialogues")]
    [SerializeField] private Dialogue dialogue;
    private int countDialogue = 0;

    public void Interact() {

        // Displays all chats in order
        if(countDialogue < dialogue.getSize()) {
            ScreenTexts.HideText();
            ScreenTexts.ShowDialogueText(dialogue.getLine(countDialogue).getName(), dialogue.getLine(countDialogue).getText());
        } else {  // Ending condition
            ScreenTexts.HideText();
            PlayerInteractions.state = InteractStates.None;
        }
        countDialogue++;
        countDialogue = countDialogue % (dialogue.getSize() + 1);
    }

    public string getName() { return nickname; }
}
