using UnityEngine;

[CreateAssetMenu(fileName = "New_PNJ", menuName = "Interactibles/PNJ", order = 1)]
public class PNJObject : ScriptableObject {
    
    [Header("PNJ Object")]
    [SerializeField] private string nickname;
    [SerializeField] private bool alreadyTalked;

    private InteractibleType type = InteractibleType.PNJ;

    public void Interact() {
        Debug.Log("SALUT, MON NOM EST " + this.nickname + ".");
    }

}
