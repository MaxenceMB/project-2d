using UnityEngine;
using System.Collections;
using TMPro;
using System;
using System.Linq;


/// <summary>
/// Enumeration used to positionate the text correctly when using ShowText()
/// </summary>
public enum TextPos { CENTER, TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, DIALOGUE_TEXT, DIALOGUE_NAME, SHOP_TEXT };


/// <summary>
/// Class to put on the Main Canvas of each scenes.
/// Calculates, stores and returns all values that are used to show text on screen.
/// </summary>
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
    [SerializeField] private Color placesColor;                             // Color of places names in dialogues


    // --- Static versions --- //
    private static GameObject    dialoguePanel, dialoguePanelPrompt, canvas;
    private static TMP_FontAsset dialogueFont;
    private static float         canvasWidth, canvasHeight;
    private static int           textSize, nameSize;
    private static Color         nameC, moneyC, placesC;
    private static bool          writing = false;   
    private static MonoBehaviour instance;


    /// <summary>
    /// Start method:
    /// - Assigns the variables to their static versions
    /// - Calculates canvas's sizes
    /// - Calculates DialoguePane sizes and places it correctly
    /// </summary>
    private void Start() {

        // Assign static values
        canvas        = gameObject;
        dialoguePanel = dialoguePanelObject;
        dialogueFont  = dialogueFontAsset;
        textSize      = textFontSize;
        nameSize      = nameFontSize;
        nameC         = nameColor;
        moneyC        = moneyColor;
        placesC       = placesColor;
        instance      = this;

        // Gets canvas size
        canvasWidth  = this.GetComponent<RectTransform>().rect.width;
        canvasHeight = this.GetComponent<RectTransform>().rect.height;

        // Places the dialogue panel correctly
        dialoguePanel.transform.position = new Vector2(0.5f*canvasWidth, 0.35f*canvasHeight);
        dialoguePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0.62f*canvasWidth, 0.4f*canvasHeight);
        dialoguePanel.SetActive(false);
        
        // Places the dialogue panel prompt correctly
        dialoguePanelPrompt = dialoguePanel.transform.GetChild(0).gameObject;
        dialoguePanelPrompt.transform.position = new Vector2(0.78f*canvasWidth, 0.20f*canvasHeight);
        dialoguePanelPrompt.SetActive(false);
    }


    /// <summary>
    /// Shows a text on screen with its correct position and size.
    /// </summary>
    /// 
    /// <param name="textToShow">  string: The text to show                                                       </param>
    /// <param name="fontSize"  >     int: The font size                                                          </param>
    /// <param name="pos"       > TextPos: The position on screen                                                 </param>
    /// <param name="charByChar">    bool: Specifies if the text will be shown directly or character by character </param>
    public static void ShowText(string textToShow, float fontSize, TextPos pos, Color? textColor = null, bool charByChar = false, bool dialogue = false) {

        // Create the text game object
        GameObject textObj = new GameObject("textObj_");        
        TMP_Text UIText    = textObj.AddComponent<TextMeshProUGUI>();
        textObj.transform.SetParent(canvas.transform);
        textObj.tag = (dialogue) ? "DialogueTextUI" : "TextUI";
        
        // Gets the position on screen
        float[] values = GetPosValues(pos);

        // Assigning the position to the game object
        textObj.transform.position = new Vector2(values[0], values[1]);
        textObj.GetComponent<RectTransform>().sizeDelta = new Vector2(values[2], values[3]);

        // Assigning text and fontsize
        UIText.fontSize    = CorrectFontSize(fontSize);
        UIText.font        = GetDialogueFont();
        UIText.alignment   = (TextAlignmentOptions)values[4];
        UIText.color       = (textColor == null) ? Color.white : (Color)textColor;
        UIText.lineSpacing = -30.0f;

        // Adds the correct colors to the text
        textToShow = ColorText(textToShow);

        // Show the text character by character if asked for
        if(charByChar) {
            writing = true;
            instance.StartCoroutine(CharByChar(UIText, textToShow));
        } else {
            UIText.text = textToShow;
        }
    }


    /// <summary>
    /// Shows the dialogue text on screen with its correct position and size (name and text).
    /// It calls ScreenTexts() twice and shows the panel.
    /// </summary>
    /// 
    /// <param name="charName"  > string: Name of the character talking                                  </param>
    /// <param name="textToShow"> string: The dialogue line text                                         </param>
    /// <param name="charByChar">   bool: Specifies if the text is been show character by character or no</param>
    public static void ShowDialogueText(string charName, string textToShow, bool charByChar) {

        // Shows the panel
        SetDialoguePanel(true);

        // Write both texts, name and text
        ShowText(charName,   nameSize, TextPos.DIALOGUE_NAME, charByChar: false,      dialogue: true);
        ShowText(textToShow, textSize, TextPos.DIALOGUE_TEXT, charByChar: charByChar, dialogue: true);
    }


    /// <summary>
    /// Hides all the texts that are currently shown on screen
    /// and hides dialogue text box if shown
    /// </summary>
    public static void HideText(bool dialogueOnly = false) {

        // Finds all the texts in scene
        GameObject[] dialogueTexts = GameObject.FindGameObjectsWithTag("DialogueTextUI");        

        // For each of them: destroy
        foreach(GameObject text in dialogueTexts) {
             GameObject.Destroy(text);
        }

        if(!dialogueOnly) {
            GameObject[] texts = GameObject.FindGameObjectsWithTag("TextUI");        
            foreach(GameObject text in texts) {
                GameObject.Destroy(text);
            }
        }

        // If dialogue box is visible, turn it off
        if(GetDialoguePanel().activeSelf) SetDialoguePanel(false);
    }


    /// <summary>
    /// Just starts the coroutine to check when the DialogueLine ends
    /// </summary>
    /// 
    /// <param name="npc"> NPCObject: Here just tell him back the line ended </param>
    public static void CheckNPCEndLine(NPCObject npc) {
        instance.StartCoroutine(CheckEndLine(npc));
    }


    /// <summary>
    /// Stops the CHARBYCHAR() coroutine
    /// </summary>
    /// 
    /// <param name="npc"> NPCObject: Here just tell him back the line ended </param>
    public static void StopCharByChar(NPCObject npc) {
        instance.StopAllCoroutines();
        npc.EndLine();
        writing = false;
    }


    /// <summary>
    /// Gives the time dialogue has to wait before showing next char, depending on the char itself
    /// </summary>
    /// 
    /// <param name="c"> char: The character the pause will be taken afdter</param>
    /// <returns> float: Time as seconds, in a float value</returns>
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


    /// <summary>
    /// Gets and returns position values on screen for each TextPos
    /// </summary>
    /// 
    /// <param name="pos"> TextPos: The position we need the values of </param>
    /// <returns> Returns a float array with : [0] = Left pos
    ///                                        [1] = Top pos
    ///                                        [2] = Width of the text box
    ///                                        [3] = Height of the text box
    ///                                    and [4] = float value of AlignmentOption enumeration
    /// </returns>
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

            case TextPos.SHOP_TEXT:
                values[0] = 0.607f * canvasWidth;
                values[1] = 0.500f * canvasHeight;
                values[2] = 0.180f * canvasWidth;
                values[3] = 0.420f * canvasHeight;

                values[4] = (float)TextAlignmentOptions.TopJustified;
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


    /// <summary>
    /// Puts the right colors attributes to the text
    /// </summary>
    /// 
    /// <param name="text"> string: The text to modify </param>
    /// <returns> string: The new correctly colored text </returns>
    private static string ColorText(string text) {

        // Initialise WAY TOO MUCH variables
        char[] array       = text.ToCharArray();    // The text as an array of character (to check it char by char)
        int    index       = 0;                     // Index of the first special character

        string start       = "";                    // String taking all the text before the special character
        string replace     = "";                    // Empty string that will contain the replacing part of the text
        string end         = "";                    // String taking all the text after the special character

        // For each character in the tex
        foreach (char letter in array) {
            switch(letter) {

                // If the character is a '%', it means it will be a color attribute after
                case '%':

                    // Takes the character just after the '%'
                    index = Array.IndexOf(array, letter);
                    char c = array[index+1];

                    // Make a string containing the word after the color tag
                    string word    = "";
                    int    indexW  = index+2;
                    char   w       = array[indexW];
                    char[] endWord = {' ', '.', ',', '!', '?'};
                    while(endWord.Contains(w) == false) {
                        word += w;
                        indexW++;
                        w = array[indexW];
                    }

                    // Saves the texts before and after
                    start   = text.Substring(0, index);
                    end     = text.Substring(indexW, text.Length - indexW);
                    replace = "";

                    switch(c) {
                        case 'n': // Name color
                            replace = "<color=" + ColorToHex(ScreenTexts.GetDialogueNameColor()) + ">" + word + "</color>";
                            text = start + replace + end;
                            array = text.ToCharArray();
                            break;


                        case 'p': // Places color
                            replace = "<color=" + ColorToHex(ScreenTexts.GetDialoguePlacesColor()) + ">" + word + "</color>";
                            text = start + replace + end;
                            array = text.ToCharArray();
                            break;


                        default:
                            break;
                    }
                    break;


                // If the character is '€', just changes its color
                case '€':

                    index = Array.IndexOf(array, letter);

                    start   = text.Substring(0, index);
                    end     = text.Substring(index+1, text.Length - (index+1));
                    replace = "<color=" + ColorToHex(ScreenTexts.GetDialogueMoneyColor()) + ">€</color>";

                    text = start + replace + end;
                    array = text.ToCharArray();

                    break;


                default:
                    break;

            }
		}

        return text;
    }


    /// <summary>
    /// Returns a color in its hexadecimal code
    /// </summary>
    /// 
    /// <param name="c"> Color: The color we want to get in Hex </param>
    /// <returns> string: the hex code of the color </returns>
    private static string ColorToHex(Color c) {
        string r = Mathf.RoundToInt(c.r * 255f).ToString("X2");
        string g = Mathf.RoundToInt(c.g * 255f).ToString("X2");
        string b = Mathf.RoundToInt(c.b * 255f).ToString("X2");

        return "#"+r+g+b;
    }


    /// <summary>
    /// Calculates the adapted font size compared to the canvas's size
    /// /!\ IMPORTANT                                                                               --- /!\
    ///     It also multiplies the size by 4 because basic font sizes for the game were at like 100+
    ///     So by multiplying by 4 the result, we can put as input values like 12, 20 etc...
    /// /!\ ---                                                                                     --- /!\
    /// </summary>
    /// 
    /// <param name="fontSize"> float: Original font size value </param>
    /// <returns> int: The new adapted font size </returns>
    public static int CorrectFontSize(float fontSize) {
        return (int)(((canvasHeight+canvasWidth)*fontSize)/3000)*4;
    }



    ////////// COROUTINES \\\\\\\\\\


    /// <summary>
    /// Coroutine to check when the DialogueLine ends
    /// </summary>
    /// 
    /// <param name="npc"> NPCObject: Here just tell him back the line ended </param>
    private static IEnumerator CheckEndLine(NPCObject npc) {    
        yield return new WaitForSeconds(0.2f);
        while(writing) {
            yield return null;
        }
        npc.EndLine();
        yield return null;
    }


    /// <summary>
    /// Shows the text character by character, including colors
    /// </summary>
    /// 
    /// <param name="txtObj"> TMP_Text: The object visible on screen, the one we modify its text </param>
    /// <param name="text"  >   string: The text that will be shown character by character       </param>
    private static IEnumerator CharByChar(TMP_Text txtObj, string text) {

        // Initialise some variables
        char savedLetter = 'a';
        string color     = "";

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



    ////////// GET AND SET FUNCTIONS \\\\\\\\\\


    /// <summary>
    /// Returns the dialogue panel as GameObject
    /// </summary>
    /// 
    /// <returns> GameObject: The dialoguePanel </returns>
    public static GameObject GetDialoguePanel() {
        return dialoguePanel;
    }


    /// <summary>
    /// Changes the dialogue panel active state
    /// </summary>
    /// 
    /// <param name="b"> bool: To set active or no the dialoguePanel </param>
    public static void SetDialoguePanel(bool b) {
        dialoguePanel.SetActive(b);
    }


    /// <summary>
    /// Changes the dialogue panel prompt active state
    /// </summary>
    /// 
    /// <param name="b"> bool: To set active or no the dialoguePanelPrompt </param>
    public static void SetDialoguePrompt(bool b) {
        dialoguePanelPrompt.SetActive(b);
    }


    /// <summary>
    /// Returns the dialogue font
    /// </summary>
    /// 
    /// <returns> TMP_FontAsset: The dialogue font </returns>
    public static TMP_FontAsset GetDialogueFont() {
        return dialogueFont;
    }


    /// <summary>
    /// Returns the correct color for showing names in dialogues
    /// </summary>
    /// 
    /// <returns> Color: The used color to write names in dialogues </returns>
    public static Color GetDialogueNameColor() {
        return nameC;
    }
    

    /// <summary>
    /// Returns the correct color for showing Pems in dialogues
    /// </summary>
    /// 
    /// <returns> Color: The used color for the Pems Symbol in dialogues </returns>
    public static Color GetDialogueMoneyColor() {
        return moneyC;
    }


    /// <summary>
    /// Returns the correct color for showing places in dialogues
    /// </summary>
    /// 
    /// <returns> Color: The used color for the Pems Symbol in dialogues </returns>
    public static Color GetDialoguePlacesColor() {
        return placesC;
    }


    /// <summary>
    /// Returns the main canvas of the scene
    /// </summary>
    /// 
    /// <returns> GameObject: The main canvas </returns>
    public static GameObject GetCanvas() {
        return canvas;
    }


    /// <summary>
    /// Returns the canvas's sizes
    /// </summary>
    /// 
    /// <returns> Returns a float array with : [0] = canvas width
    ///                                    and [1] = canvas height </returns>
    public static float[] GetCanvasSize() {
        float[] values = {canvasWidth, canvasHeight};
        return values;
    }


    /// <summary>
    /// Returns if text is still being written
    /// </summary>
    /// 
    /// <returns> bool: True if the text is still being written </returns>
    public static bool IsWriting() {
        return writing;
    }

}
