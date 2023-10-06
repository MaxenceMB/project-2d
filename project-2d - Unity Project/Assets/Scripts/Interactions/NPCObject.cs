using UnityEngine;

[CreateAssetMenu(fileName = "New_NPC", menuName = "Interactibles/NPC/New NPC")]
public class NPCObject : ScriptableObject {
    
    [Header("PNJ Object")]
    [SerializeField]  private string nickname;
    [HideInInspector] public  bool   canInteract = true;

    [Header("Dialogues")]
    [SerializeField] private Dialogue dialogue;
    private int countDialogue = 0;

    private PlayerMovement pm;


    /// <summary>
    /// 
    /// </summary>
    private void OnEnable() {
        countDialogue = 0;
    }


    /// <summary>
    /// 
    /// </summary>
    public void Interact() {
        if(pm == null) LoadPM();

        ScreenTexts.HideText(true);
        ScreenTexts.SetDialoguePrompt(false);

        // Displays all chats in order
        if(countDialogue < dialogue.getSize()) {
            pm.SetCanMove(false);

            if(ScreenTexts.IsWriting()) {
                ScreenTexts.StopCharByChar(this);
                ScreenTexts.ShowDialogueText(dialogue.getLine(countDialogue-1).getName(), dialogue.getLine(countDialogue-1).getText(), false);
            } else {
                ScreenTexts.ShowDialogueText(dialogue.getLine(countDialogue).getName(), dialogue.getLine(countDialogue).getText(), true);
                ScreenTexts.CheckNPCEndLine(this);
            }
        } else {
            pm.SetCanMove(true);
            countDialogue = 0;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void EndLine() {
        ScreenTexts.SetDialoguePrompt(true);
        countDialogue++;
    }


    /// <summary>
    /// 
    /// </summary>
    private void LoadPM() {
        pm = GameObject.Find("Player").gameObject.GetComponent<PlayerMovement>();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string getName() { return nickname; }
}
