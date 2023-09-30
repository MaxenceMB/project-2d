using UnityEngine;
using System;

[System.Serializable]
public class DialogueLine {
    
    [Header("Dialogue Line Object")]
    [SerializeField] private NPCObject character;
    [SerializeField] [TextAreaAttribute] private string textLine;

    public NPCObject getPNJ()  { return character;            }
    public string    getName() { return character.getName();  }

    public string getText() {         
        
        char[] array = textLine.ToCharArray();

        string start   = "";
        string end     = "";
        string replace = "";

        bool charSpe = false;

        int index = 0;

        foreach (char letter in array) {
            switch(letter) {
                case '$':
                    charSpe = true;

                    index = Array.IndexOf(array, letter);
                    char c = array[index+1];

                    start   = textLine.Substring(0, index);
                    end     = textLine.Substring(index+2, textLine.Length - (index+2));
                    replace = "";

                    switch(c) {
                        case 'n':
                            replace = "<color=" + ToHex(ScreenTexts.GetDialogueNameColor()) + ">" + character.getName() + "</color>";
                            break;

                        default:
                            replace = "AAAAAAAAAAAAAAAAAAAAAAAAA";
                            break;
                    }
                    break;


                case '€':
                    charSpe = true;

                    index = Array.IndexOf(array, letter);

                    start   = textLine.Substring(0, index);
                    end     = textLine.Substring(index+1, textLine.Length - (index+1));
                    replace = "<color=" + ToHex(ScreenTexts.GetDialogueMoneyColor()) + ">€</color>";

                    break;

                default:
                    break;

            }
		}

        if(charSpe) {
            return start + replace + end;
        } else {
            return textLine;
        }
    }

    private string FloatNormalizedToHex(float f) {
        return Mathf.RoundToInt(f * 255f).ToString("X2");
    }

    private string ToHex(Color c) {
        string r = FloatNormalizedToHex(c.r);
        string g = FloatNormalizedToHex(c.g);
        string b = FloatNormalizedToHex(c.b);

        return "#"+r+g+b;
    }
    
}