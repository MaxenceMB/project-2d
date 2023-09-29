using UnityEngine;

[CreateAssetMenu(fileName = "New_Sign", menuName = "Interactibles/New Sign")]
public class SignObject : ScriptableObject {
    
    [Header("Sign Object")]
    [SerializeField] private string title;

    [Header("Sign Text")]
    [SerializeField][TextArea(5, 20)] private string signText;
    private bool reading = false;


    public void Interact() {

        // Displays all chats in order
        if(!reading) {
            ScreenTexts.ShowText(signText, 50, TextPos.CENTER, true);
            reading = true;
        } else {  // Ending condition
            ScreenTexts.HideText();
            reading = false;
            
            PlayerInteractions.state = InteractStates.End;
        }
    }

}
