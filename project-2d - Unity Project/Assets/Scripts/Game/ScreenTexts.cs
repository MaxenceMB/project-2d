<<<<<<< Updated upstream
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TextPos { CENTER, TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, DIALOGUE_TEXT, DIALOGUE_NAME }

public class ScreenTexts : MonoBehaviour {

    private static GameObject canvas;
    private static ScreenTextsStats sts;
    public static float canvasWidth, canvasHeight;

    private static MonoBehaviour instance;

    private void Start() {
        // Get canvas and store its dimensions
        canvas = GameObject.Find("DialogueCanvas");
        canvasWidth  = canvas.GetComponent<RectTransform>().rect.width;
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;

        // Takes screen text values from STS script
        sts = canvas.GetComponent<ScreenTextsStats>();

        instance = this;
    }

    public static void ShowText(string textToShow, float fontSize, TextPos pos, bool charByChar) {

        // Create the text game object
        GameObject textObj = new GameObject("textObj_");
        textObj.transform.SetParent(canvas.transform);
        textObj.tag = "TextUI";
        TMP_Text UIText = textObj.AddComponent<TextMeshProUGUI>();

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
        UIText.font     = sts.textFont;

        if(charByChar) {
            instance.StartCoroutine(CharByChar(UIText, textToShow));
        } else {
            UIText.text = textToShow;
        }
    }

    public static void ShowDialogueText(string charName, string textToShow) {

        // Shows the panel
        sts.dialoguePanel.SetActive(true);

        // Write both texts, name and text
        ShowText(charName,   sts.dialogueNameSize, TextPos.DIALOGUE_NAME, false);
        ShowText(textToShow, sts.dialogueTextSize, TextPos.DIALOGUE_TEXT, true );

    }

    public static void HideText() {
        GameObject[] texts = GameObject.FindGameObjectsWithTag("TextUI");
        foreach(GameObject text in texts) {
            GameObject.Destroy(text);
        }

        if(sts.dialoguePanel.activeSelf) sts.dialoguePanel.SetActive(false);
    }
    
    private static IEnumerator CharByChar(TMP_Text txtObj, string text) {
        char savedLetter = 'a';
        string color = "";

		foreach (char letter in text.ToCharArray()) {
            if(letter == '<' || savedLetter == '<') {
                savedLetter = '<';

                if(letter == '>'){
                    savedLetter = '>';

                    color += letter;
                    txtObj.text += color;

                    color = "";
                } else {
                    color += letter;
                }
            } else {
			    txtObj.text += letter;
			    yield return new WaitForSeconds(sts.getPauseChar(letter));
            }
		}
	}

}
=======
using UnityEngine;
using System.Collections;
using TMPro;


// Enumeration of text positions on screen
public enum TextPos { CENTER, TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, DIALOGUE_TEXT, DIALOGUE_NAME };


// SCREENTEXTSSTATS - Class to put on the Main Canvas of each scenes.
// Calculates, stores and returns all values that are used in SCREENTEXTS static class.
public class ScreenTexts : MonoBehaviour {

    // ---  Serializable versions --- //
    [Header("Dialogues")]
    [SerializeField] private GameObject dialoguePanelObject;                // GameObject UI.Panel that gets enabled when some dialogue is showing
    [Space(10)]
    [SerializeField] private TMP_FontAsset dialogueFontAsset;               // TextMeshPro FontAsset / Main font of dialogues
    [Space(10)]
    [SerializeField] [Range(1, 100)] private int textFontSize;              // Raw text size of the text in dialogues
    [SerializeField] [Range(1, 100)] private int nameFontSize;              // Raw text siee of the character names in dialogues
    [Space(10)]
    [SerializeField] private Color nameColor;                               // Color of character names in dialogues
    [SerializeField] private Color moneyColor;                              // Color of Pems coins character in dialogues


    // --- Static versions --- //
    private static GameObject    dialoguePanel, canvas;
    private static TMP_FontAsset dialogueFont;
    private static float         canvasWidth, canvasHeight;
    private static int           textSize, nameSize;
    private static Color         nameC, moneyC;
    private static bool          writing = false;   
    private static MonoBehaviour instance;


    // VOID START() :
    // ---                                                  ---
    // Loads on start of the scene and calculates some values
    // ---                                                  ---
    private void Start() {

        // Assign static values
        canvas        = gameObject;
        dialoguePanel = dialoguePanelObject;
        dialogueFont  = dialogueFontAsset;
        textSize      = textFontSize;
        nameSize      = nameFontSize;
        nameC         = nameColor;
        moneyC        = moneyColor;
        instance      = this;

        // Gets canvas size
        canvasWidth  = this.GetComponent<RectTransform>().rect.width;
        canvasHeight = this.GetComponent<RectTransform>().rect.height;

        // Places the dialogue panel correctly
        dialoguePanel.transform.position = new Vector2(0.5f*canvasWidth, 0.35f*canvasHeight);
        dialoguePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0.62f*canvasWidth, 0.4f*canvasHeight);
        dialoguePanel.SetActive(false);
    }


    // VOID SHOWTEXT(STRING, FLOAT, TEXTPOS, BOOL) :
    // ---                                                                                    ---
    // Shows a text on screen with its correct position and size
    // Takes 4 arguments: 
    //                  - STRING  for the text to show
    //                  - FLOAT   for the font size
    //                  - TEXTPOS for the position on screen
    //                  - BOOL    to specify if the text will be shown directly or char by char
    // ---                                                                                    ---
    public static void ShowText(string textToShow, float fontSize, TextPos pos, bool charByChar) {

        // Create the text game object
        GameObject textObj = new GameObject("textObj_");        
        TMP_Text UIText    = textObj.AddComponent<TextMeshProUGUI>();
        textObj.transform.SetParent(canvas.transform);
        textObj.tag = "TextUI";
        
        // Gets the position on screen
        float[] values = GetPosValues(pos);

        // Assigning the position to the game object
        textObj.transform.position = new Vector2(values[0], values[1]);
        textObj.GetComponent<RectTransform>().sizeDelta = new Vector2(values[2], values[3]);

        // Assigning text and fontsize
        UIText.fontSize  = fontSize;
        UIText.font      = GetDialogueFont();
        UIText.alignment = (TextAlignmentOptions)values[4];

        // Show the text character by character if asked for
        if(charByChar) {
            writing = true;
            instance.StartCoroutine(CharByChar(UIText, textToShow));
        } else {
            UIText.text = textToShow;
        }
    }


    // SHOWDIALOGUETEXT(STRING, STRING, BOOL) :
    // ---                                                                                    ---
    // Shows the dialogue text on screen with its correct position and size (name and text)
    // It calls SHOWTEXT() twice and shows the panel
    // Takes 3 arguments: 
    //                  - STRING  for the name of the character
    //                  - STRING  for the text to show
    //                  - BOOL    to specify if the text will be shown directly or char by char
    // ---                                                                                    ---
    public static void ShowDialogueText(string charName, string textToShow, bool charByChar) {

        // Shows the panel
        SetDialoguePanel(true);

        // Write both texts, name and text
        ShowText(charName,   GetDialogueNameSize(), TextPos.DIALOGUE_NAME, false);
        ShowText(textToShow, GetDialogueTextSize(), TextPos.DIALOGUE_TEXT, charByChar);
    }


    // HIDETEXT() :
    // ---                                                  ---
    // Hides all the texts that are currently shown on screen
    // (And hides dialogue text box if shown)
    // ---                                                  ---
    public static void HideText() {

        // Finds all the texts in scene
        GameObject[] texts = GameObject.FindGameObjectsWithTag("TextUI");

        // For each of them: destroy
        foreach(GameObject text in texts) {
            GameObject.Destroy(text);
        }

        // If dialogue box is visible, turn it off
        if(GetDialoguePanel().activeSelf) SetDialoguePanel(false);
    }


    // STATIC CHECKNPCENDLINE(NPCOBJECT) :
    // ---                                                               ---
    // Just starts the coroutine to check when the DialogueLine ends
    // Takes a NPCObject as argument, to just tell him back the line ended
    // ---                                                               ---   
    public static void CheckNPCEndLine(NPCObject npc) {
        instance.StartCoroutine(CheckEndLine(npc));
    }


    // STATIC STOPCHARBYCHAR() :
    // ---                            ---
    // Stops the CHARBYCHAR() coroutine
    // ---                            ---
    public static void StopCharByChar(NPCObject npc) {
        instance.StopAllCoroutines();
        Debug.Log("STOPPED COROUTINES");
        npc.EndLine();
        writing = false;
    }

    // COROUTINE CHECKENDLINE(NPCOBJECT) :
    // ---                                                                      ---
    // Coroutine to check when the DialogueLine ends
    // Takes a NPCObject as argument and starts NPCOBJECT.ENDLINE() when finished
    // ---                                                                      ---   
    private static IEnumerator CheckEndLine(NPCObject npc) {    
        yield return new WaitForSeconds(0.2f);
        while(writing) {
            yield return null;
        }
        Debug.Log("Text Ended");
        npc.EndLine();
        yield return null;
    }


    // COROUTINE CHARBYCHAR(TMP_TEXT, STRING) :
    // ---                                                   ---
    // Shows the text character by character, including colors
    // Takes 2 arguments:
    //                  - TMP_TEXT to modify the text on UI
    //                  - STRING   the text that will be shown
    // ---                                                   ---
    private static IEnumerator CharByChar(TMP_Text txtObj, string text) {

        // Initialise some variables
        char savedLetter = 'a';
        string color = "";

        // For each character in the text
		foreach (char letter in text.ToCharArray()) {

            // If the there is a color atribute -> skip it
            if(letter == '<' || savedLetter == '<') {
                savedLetter = '<';

                if(letter == '>'){
                    savedLetter = '>';

                    color += letter;
                    txtObj.text += color;

                    color = "";
                } else {
                    color += letter;
                }

            } else {
                // Add the letter to the UI text field
			    txtObj.text += letter;
			    yield return new WaitForSeconds(GetPauseChar(letter));
            }
		}

        // Turns the variable isWriting to false to know it ended
        writing = false;
	}


    // FLOAT[] GETPOSVALUES(TEXTPOS) :
    // ---                                                                         ---
    // Gets and returns position values on screen for each TextPos
    // Takes a TextPos argument
    // Returns a float array with : [0] = Left pos
    //                              [1] = Top pos
    //                              [2] = Width of the text box
    //                              [3] = Height of the text box
    //                          and [4] = float value of AlignmentOption enumeration
    // ---                                                                         ---
    private static float[] GetPosValues(TextPos pos) {
        float[] values = new float[5];

        switch(pos) {
            case TextPos.TOP_LEFT:
                values[0] = 0.25f * canvasWidth;
                values[1] = 0.98f * canvasHeight;
                values[2] = 0.50f * canvasWidth;
                values[3] = 0.04f * canvasHeight;

                values[4] = (float)TextAlignmentOptions.TopLeft;
                break;
            
            case TextPos.TOP_RIGHT:
                values[0] = 0.75f * canvasWidth;
                values[1] = 0.98f * canvasHeight;
                values[2] = 0.50f * canvasWidth;
                values[3] = 0.04f * canvasHeight;

                values[4] = (float)TextAlignmentOptions.TopRight;
                break;

            case TextPos.BOTTOM_LEFT:
                values[0] = 0.25f * canvasWidth;
                values[1] = 0.02f * canvasHeight;
                values[2] = 0.50f * canvasWidth;
                values[3] = 0.04f * canvasHeight;

                values[4] = (float)TextAlignmentOptions.BottomLeft;
                break;

            case TextPos.BOTTOM_RIGHT:
                values[0] = 0.75f * canvasWidth;
                values[1] = 0.02f * canvasHeight;
                values[2] = 0.50f * canvasWidth;
                values[3] = 0.04f * canvasHeight;

                values[4] = (float)TextAlignmentOptions.BottomRight;
                break;

            case TextPos.DIALOGUE_TEXT:
                values[0] = 0.50f * canvasWidth;
                values[1] = 0.30f * canvasHeight;
                values[2] = 0.60f * canvasWidth;
                values[3] = 0.275f* canvasHeight;

                values[4] = (float)TextAlignmentOptions.TopLeft;
                break;

            case TextPos.DIALOGUE_NAME:
                values[0] = 0.50f * canvasWidth;
                values[1] = 0.50f * canvasHeight;
                values[2] = 0.60f * canvasWidth;
                values[3] = 0.10f * canvasHeight;

                values[4] = (float)TextAlignmentOptions.Left;
                break;

            default:
                values[0] = 0.50f * canvasWidth;
                values[1] = 0.50f * canvasHeight;
                values[2] = 1.00f * canvasWidth;
                values[3] = 0.25f * canvasHeight;

                values[4] = (float)TextAlignmentOptions.Center;
                break;
        }

        return values;
    }


    // FLOAT GETPAUSECHAR(CHAR) :
    // ---                                                                                      ---
    // Gives the time dialogue has to wait before showing next char, depending on the char itself
    // Takes a char (current one)   
    // Returns time as seconds, in a float value
    // ---                                                                                      ---
    private static float GetPauseChar(char c) {
        switch(c) {
            case '.':
                return 0.30f;

            case '!':
                return 0.30f;

            case '?':
                return 0.50f;

            case ',':
                return 0.10f;

            default:
                return 0.05f;

        }
    }


    // GAMEOBJECT GETDIALOGUEPANEL() :
    // ---                                    ---
    // Returns the dialogue panel as GameObject
    // ---                                    ---
    public static GameObject GetDialoguePanel() {
        return dialoguePanel;
    }


    // GAMEOBJECT SETDIALOGUEPANEL(BOOL) :
    // ---                                   ---
    // Changes the dialogue panel active state
    // ---                                   ---
    public static void SetDialoguePanel(bool b) {
        dialoguePanel.SetActive(b);
    }


    // TMP_FONTASSER GETDIALOGUEFONT() :
    // ---                                             ---
    // Returns the dialogue font (TextMeshPro FontAsset)
    // ---                                             ---
    public static TMP_FontAsset GetDialogueFont() {
        return dialogueFont;
    }


    // INT GETDIALOGUENAMESIZE() :
    // ---                                       ---
    // Returns the dialogue font size for the name
    // ---                                       ---
    public static int GetDialogueNameSize() {
        return nameSize;
    }


    // INT GETDIALOGUETEXTSIZE() :
    // ---                                       ---
    // Returns the dialogue font size for the text
    // ---                                       ---
    public static int GetDialogueTextSize() {
        return textSize;
    }


    // COLOR GETDIALOGUENAMECOLOR() :
    // ---                                                    ---
    // Returns the correct color for showing names in dialogues
    // ---                                                    ---
    public static Color GetDialogueNameColor() {
        return nameC;
    }
    

    // COLOR GETDIALOGUEMONEYCOLOR() :
    // ---                                                   ---
    // Returns the correct color for showing Pems in dialogues
    // ---                                                   ---
    public static Color GetDialogueMoneyColor() {
        return moneyC;
    }


    // GAMEOBJECT GETCANVAS() :
    // ---                                ---
    // Returns the main canvas of the scene
    // ---                                ---
    public static GameObject GetCanvas() {
        return canvas;
    }


    // FLOAT[] GETCANVASSIZE() :
    // ---                                            ---
    // Returns a float array with : [0] = canvas width
    //                          and [1] = canvas height
    // ---                                            ---
    public static float[] GetCanvasSize() {
        float[] values = {canvasWidth, canvasHeight};
        return values;
    }


    public static bool IsWriting() {
        return writing;
    }

}
>>>>>>> Stashed changes
