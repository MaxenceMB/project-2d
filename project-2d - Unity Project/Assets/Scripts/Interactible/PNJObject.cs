using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New_PNJ", menuName = "Interactibles/PNJ")]
public class PNJObject : ScriptableObject {
    
    [Header("PNJ Object")]
    [SerializeField] private string nickname;
    [SerializeField] private bool alreadyTalked;

    [Header("Dialogues")]
    [SerializeField] private string[] dialogues;
    private int countDialogue;

    public void Interact() {

        // Displays all chats in order
        if(countDialogue < dialogues.Length) {
            ScreenTexts.HideText();
            ScreenTexts.ShowDialogueText(nickname, dialogues[countDialogue]);
        } else {  // Ending condition
            ScreenTexts.HideText();
            PlayerInteractions.state = InteractStates.None;
        }
        countDialogue++;
        countDialogue = countDialogue % 3;
    }
}
