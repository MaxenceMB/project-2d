using UnityEngine;

[CreateAssetMenu(fileName = "New_PNJ", menuName = "Interactibles/PNJ")]
public class PNJObject : ScriptableObject {
    
    [Header("PNJ Object")]
    [SerializeField] private string nickname;
    [SerializeField] private bool alreadyTalked;

    public void Interact() {
        Debug.Log("SALUT, MON NOM EST " + this.nickname + ".");
    }
}
