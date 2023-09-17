using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TextPos { CENTER, TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, DIALOGUE_TEXT, DIALOGUE_NAME }

public class ScreenTexts : MonoBehaviour {

    private static GameObject canvas;
    private static float canvasWidth, canvasHeight; 
    private static ScreenTextsStats sts;

    private void Start() {
        canvas = GameObject.Find("Main Canvas");
        canvasWidth  = canvas.GetComponent<RectTransform>().rect.width;
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;

        sts = canvas.GetComponent<ScreenTextsStats>();
    }

    public static void ShowText(string textToShow, float fontSize, TextPos pos) {

        // Create the text game object
        GameObject textObj = new GameObject("textObj_");
        textObj.transform.SetParent(canvas.transform);
        textObj.tag = "TextUI";

        // Add the text
        TMP_Text UIText = textObj.AddComponent<TextMeshProUGUI>();
        UIText.text = textToShow;

        // Modifies the properties
        float posLeft, posTop, boxWidth, boxHeight;
        
        switch(pos) {
            case TextPos.TOP_LEFT:
                posLeft   = 0.25f;
                posTop    = 0.98f;
                boxWidth  = 0.50f;
                boxHeight = 0.04f;

                UIText.alignment = TextAlignmentOptions.TopLeft;
                break;
            
            case TextPos.TOP_RIGHT:
                posLeft   = 0.75f;
                posTop    = 0.98f;
                boxWidth  = 0.50f;
                boxHeight = 0.04f;

                UIText.alignment = TextAlignmentOptions.TopRight;
                break;

            case TextPos.BOTTOM_LEFT:
                posLeft   = 0.25f;
                posTop    = 0.02f;
                boxWidth  = 0.50f;
                boxHeight = 0.04f;

                UIText.alignment = TextAlignmentOptions.BottomLeft;
                break;

            case TextPos.BOTTOM_RIGHT:
                posLeft   = 0.75f;
                posTop    = 0.02f;
                boxWidth  = 0.50f;
                boxHeight = 0.04f;

                UIText.alignment = TextAlignmentOptions.BottomRight;
                break;

            case TextPos.DIALOGUE_TEXT:
                posLeft   = 0.50f;
                posTop    = 0.30f;
                boxWidth  = 0.60f;
                boxHeight = 0.275f;

                UIText.alignment = TextAlignmentOptions.TopLeft;
                break;

            case TextPos.DIALOGUE_NAME:
                posLeft   = 0.50f;
                posTop    = 0.50f;
                boxWidth  = 0.60f;
                boxHeight = 0.10f;

                UIText.alignment = TextAlignmentOptions.Left;
                break;

            default:
                posLeft   = 0.50f;
                posTop    = 0.50f;
                boxWidth  = 1.00f;
                boxHeight = 0.25f;

                UIText.alignment = TextAlignmentOptions.Center;
                break;
        }

        textObj.transform.position = new Vector2(posLeft*canvasWidth, posTop*canvasHeight);
        textObj.GetComponent<RectTransform>().sizeDelta = new Vector2(boxWidth*canvasWidth, boxHeight*canvasHeight);

        UIText.fontSize = fontSize;
    }

    public static void ShowDialogueText(string charName, string textToShow) {

        // Shows the panel
        sts.dialoguePanel.SetActive(true);

        // Write both texts, name and text
        ShowText(charName,   sts.dialogueNameSize, TextPos.DIALOGUE_NAME);
        ShowText(textToShow, sts.dialogueTextSize, TextPos.DIALOGUE_TEXT);

    }

    public static void HideText() {
        GameObject[] texts = GameObject.FindGameObjectsWithTag("TextUI");
        foreach(GameObject text in texts) {
            GameObject.Destroy(text);
        }

        if(sts.dialoguePanel.activeSelf) sts.dialoguePanel.SetActive(false);
    }
}
