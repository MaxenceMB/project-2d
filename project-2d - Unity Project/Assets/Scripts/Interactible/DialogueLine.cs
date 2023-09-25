using UnityEngine;
using System;

[System.Serializable]
public class DialogueLine {
    
    [Header("Dialogue Line Object")]
    [SerializeField] private PNJObject character;
    [SerializeField] [TextAreaAttribute] private string textLine;

    public PNJObject getPNJ()  { return character;            }
    public string    getName() { return character.getName();  }

    
    public string getText() { 
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
                        replace = character.getName();
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

}