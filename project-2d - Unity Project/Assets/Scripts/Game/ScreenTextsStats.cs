using UnityEngine;

public class ScreenTextsStats : MonoBehaviour {

    [Header("Dialogues")]
    [SerializeField] public GameObject dialoguePanel;
    [Space(10)]
    [SerializeField] [Range(1, 100)] public int dialogueTextSize;
    [SerializeField] [Range(1, 100)] public int dialogueNameSize;

    private void Start() {
        dialoguePanel.transform.position = new Vector2(0.5f*ScreenTexts.canvasWidth, 0.35f*ScreenTexts.canvasHeight);
        dialoguePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0.62f*ScreenTexts.canvasWidth, 0.4f*ScreenTexts.canvasHeight);

        dialoguePanel.SetActive(false);
    }

}
