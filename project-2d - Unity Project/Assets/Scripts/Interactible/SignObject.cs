using UnityEngine;

[CreateAssetMenu(fileName = "New_Sign", menuName = "Interactibles/New Sign")]
public class SignObject : ScriptableObject {
    
    [Header("Sign Object")]
    [SerializeField] private string title;

    [Header("Sign Text")]
    [SerializeField][TextArea(5, 20)] private string signText;
    private bool reading = false;

    private PlayerMovement pm;

    private void LoadPM() {
        pm = GameObject.Find("Player").gameObject.GetComponent<PlayerMovement>();
    }

    public void Interact() {
        if(pm == null) LoadPM();

        // Displays all chats in order
        if(!reading) {
            pm.SetCanMove(false);
            ScreenTexts.ShowText(signText, 50, TextPos.CENTER, charByChar: true);
            reading = true;
        } else {  // Ending condition
            pm.SetCanMove(true);
            ScreenTexts.HideText();
            reading = false;
        }
    }

}
