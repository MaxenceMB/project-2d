using UnityEngine;

public class ScreenTextsStats : MonoBehaviour {

    [Header("Dialogues")]
    [SerializeField] public GameObject dialoguePanel;
    [Space(10)]
    [SerializeField] [Range(1, 100)] public int dialogueTextSize;
    [SerializeField] [Range(1, 100)] public int dialogueNameSize;

    private void Start() {
        dialoguePanel.SetActive(false);
    }

}
