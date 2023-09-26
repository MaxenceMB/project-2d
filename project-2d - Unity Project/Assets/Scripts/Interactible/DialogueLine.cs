using UnityEngine;
using System;

[System.Serializable]
public class DialogueLine {
    
    [Header("Dialogue Line Object")]
    [SerializeField] private PNJObject character;
    [SerializeField] [TextAreaAttribute] private string textLine;

    private ScreenTextsStats sts;

    public PNJObject getPNJ()  { return character;            }
    public string    getName() { return character.getName();  }

    public string getText() { 
        if(sts == null) {
            LoadSTS();
        }

        char[] array = textLine.ToCharArray();

        string start   = "";
        string end     = "";
        string replace = "";

        bool charSpe = false;

        foreach (char letter in array) {
            if(letter == '$') {
                charSpe = true;

                int index = Array.IndexOf(array, letter);
                char c = array[index+1];

                start   = textLine.Substring(0, index);
                end     = textLine.Substring(index+2, textLine.Length - (index+2));
                replace = "";

                switch(c) {
                    case 'n':
                        replace = "<color=" + ToHex(sts.nameColor) + ">" + character.getName() + "</color>";
                        break;

                    default:
                        replace = "AAAAAAAAAAAAAAAAAAAAAAAAA";
                        break;
                }
            }
		}

        if(charSpe) {
            return start + replace + end;
        } else {
            return textLine;
        }
    }

    private void LoadSTS() {
        sts = GameObject.Find("Main Canvas").GetComponent<ScreenTextsStats>();
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