using UnityEngine;

[CreateAssetMenu(fileName = "New_Dialogue", menuName = "Interactibles/PNJ/New Dialogue")]
public class Dialogue : ScriptableObject {
    
    [Header("Dialogue Object")]
    [SerializeField] private DialogueLine[] lines;

    public DialogueLine getLine(int n) { return lines[n];     }  // Return the n-th Dialogue line
    public int          getSize()      { return lines.Length; }  // Return the number of lines the dialogue has

}