using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenTexts : MonoBehaviour {

    private static GameObject canvas, textObj;
    private static float canvasWidth, canvasHeight; 

    private void Start() {
        canvas = GameObject.Find("Main Canvas");

        canvasWidth  = canvas.GetComponent<RectTransform>().rect.width;
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
    }

    public static void ShowText(string textToShow, float fontSize) {

        // Create the text game object
        textObj = new GameObject("textObj");
        textObj.transform.SetParent(canvas.transform);

        // Add the text
        TMP_Text UIText = textObj.AddComponent<TextMeshProUGUI>();
        UIText.text = textToShow;

        // Modifies the properties
        textObj.transform.position = new Vector2(textObj.transform.position.x + 0.5f*canvasWidth, textObj.transform.position.y + 0.15f*canvasHeight);
        textObj.GetComponent<RectTransform>().sizeDelta = new Vector2(0.8f*canvasWidth, 0.25f*canvasHeight);

        UIText.fontSize = fontSize;
        UIText.alignment = TextAlignmentOptions.Center;
    }

    public static void HideText() {
        Destroy(textObj);
    }
}
