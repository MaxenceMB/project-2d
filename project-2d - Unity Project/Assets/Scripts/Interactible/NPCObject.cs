using UnityEngine;

[CreateAssetMenu(fileName = "New_NPC", menuName = "Interactibles/NPC/New NPC")]
public class NPCObject : ScriptableObject {
    
    [Header("PNJ Object")]
    [SerializeField] private string nickname;

    [Header("Dialogues")]
    [SerializeField] private Dialogue dialogue;
    private int countDialogue = 0;

    private void OnEnable() {
        countDialogue = 0;
    }

    public void Interact() {
        ScreenTexts.HideText();
        ScreenTexts.SetDialoguePrompt(false);

        // Displays all chats in order
        if(countDialogue < dialogue.getSize()) {
            if(ScreenTexts.IsWriting()) {
                ScreenTexts.StopCharByChar(this);
                ScreenTexts.ShowDialogueText(dialogue.getLine(countDialogue-1).getName(), dialogue.getLine(countDialogue-1).getText(), false);
            } else {
                ScreenTexts.ShowDialogueText(dialogue.getLine(countDialogue).getName(), dialogue.getLine(countDialogue).getText(), true);
                ScreenTexts.CheckNPCEndLine(this);
            }
        } else {
            PlayerInteractions.state = InteractStates.End;
            countDialogue = 0;
        }
    }

    public void EndLine() {
        ScreenTexts.SetDialoguePrompt(true);
        countDialogue++;
    }

    public string getName() { return nickname; }
}
