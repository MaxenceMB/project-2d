using UnityEngine;

[CreateAssetMenu(fileName = "New_Sign", menuName = "Interactibles/Sign")]
public class SignObject : ScriptableObject {
    
    [Header("Sign Object")]
    [SerializeField] private string title;
    [SerializeField] private bool alreadyRead;

    [Header("Sign Text")]
    [SerializeField][TextArea(5, 20)] private string signText;

    public void Interact() {
        Debug.Log(signText);

        if(!this.alreadyRead) {
            this.alreadyRead = true;
        }
    }

}
