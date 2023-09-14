using UnityEngine;

[CreateAssetMenu(fileName = "New_Sign", menuName = "Interactibles/Sign", order = 1)]
public class SignObject : ScriptableObject {
    
    [Header("Sign Object")]
    [SerializeField] private string title;
    [SerializeField] private bool alreadyRead;

    [Header("Sign Text")]
    [SerializeField][TextArea(5, 20)] private string signText;

    private InteractibleType type = InteractibleType.Sign;

    public void Interact() {
        Debug.Log(signText);
    }

}
